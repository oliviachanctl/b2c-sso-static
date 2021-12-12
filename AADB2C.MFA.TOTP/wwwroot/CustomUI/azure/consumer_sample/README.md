# Consumer Sample UI

## This documentation has moved to: https://ctl.atlassian.net/wiki/spaces/FSTM/pages/672255116173/Azure+B2C+Onboarding+-+User+Interface


These files provide a sample CenturyLink branded UI using [Chi](https://lib.lumen.com/chi/)  that includes comments and some TODO placeholders.

## Structure

There are seven HTML template files for different types of Azure B2C content (see ["Azure B2C Injected Content"](#Azure-B2C-Injected-Content) below). Not all of these template files are currently used by the Azure B2C implementation. **At a minimum** you should plan to host the `unified.html` file which is your sign in page. The others can be hosted as required. Azure B2C will use a simple CenturyLink branded default UI for any files that are needed (e.g. `exception.html`) and that you have chosen not to host.

Additionally, a sample `signoff.html` page is included that can be used to provide a consistent endpoint after the user signs out (as an alternative to returning the user to the sign in page). It also demonstrates an advanced mechanism to support multiple front channel logout URIs where necessary (see ["Single Log Out"](#Single-Log-Out) below).

To reduce duplication [nginx ssi](http://nginx.org/en/docs/http/ngx_http_ssi_module.html) (server side include) is used to insert content from the ssi directory. A real application can use static or dynamically built pages instead if required.

To reduce http request traffic most images and styles are inline. A real application can link externally instead if required.


## Azure B2C Injected Content

Azure B2C will load one of the six sample HTML template files using AJAX and then inject content into the `div` with an id of "api".

Since Azure B2C controls the injected content it does not contain the class names and structure that is required in order for the Chi styles to be applied. To work around this each page has JavaScript that modifies the content to insert Chi class names and structures.

This approach avoids the need for excessive editing of the default Azure B2C styles and therefore the ongoing (complex) maintenance that would require. However, it is unlikely that all possible Azure B2C injected content has been included in the Chi class name adjustments applied. If any injected content does not match expected Chi styles then adjustment of the page specific JavaScript can be made as required.


## Deployment

The HTML template files **must** be deployed to a CORS enabled internet based location (see ["CORS"](#CORS) below). If SSL is used the certificate **must** be valid.

Some files require modification before they are ready for use in specific environments. Review the "TODO" items in the following sample pages:

1. `ssi/head_links.html`
1. `ssi/header.html`
1. `unified.html` (this is the sign in page)
1. `signoff.html`
   
You should plan to host environment specific versions of the pages that Azure B2C must link to (e.g. production, test1-4, dev1-4 etc.) A more advanced potential alternative is to use JavaScript to adjust the UI based on the environment. Contact the Azure B2C Team for more details.

You may also need to consider any globalization requirements for your application (see ["Language Support"](#Language-Support) below).

The location of the deployed HTML template files and any environment specific or localized variants **must** be provided to the Azure B2C Team in order for your application to be correctly configured.

The HTML template files should be placed in your regular source control system. The Azure B2C Team does **not** manage HTML template files.

Beware that attempting to view the deployed pages directly in a browser will not be 100% successful. Obviously the Azure B2C injected content will be missing, but some other issues will also be noticed (e.g. when they are loaded directly some JavaScript errors will be present because jQuery has not been included).


## Single Log Out

Single Log Out (SLO) essentially means that when the user issues a logout request from any application, it will not only log out the user from that application, but also from the IDP and from any other application that has been accessed via SSO. The main steps involved are:

1. An application (Relying Party) makes an appropriately structured request to the IDP's `end_session_endpoint` including a `post_logout_redirect_uri` parameter.
1. The IDP issues a request to the configured front channel logout URI of each Relying Party with an active SSO session.
1. The IDP redirects the user to the provided `post_logout_redirect_uri`.

There are typically two choices for the `post_logout_redirect_uri`:

* Redirect the user to a protected resource in the application and therefore arrive back at the sign in page.
* Redirect the user to a page indicating that they have successfully logged out. The sample file `signoff.html` provides a simple example.

There is an additional **advanced** use case for the sample file `signoff.html` that facilitates SLO when an application requires multiple front channel logout URIs. It is generally **not recommended** and is complex to implement because it relies on all Relying Parties that share an SSO session to co-ordinate their use of a common or shared `signoff.html` file. This is described in the README.md file in the [Azure B2C on-boarding tutorial](https://github.com/CenturyLink/b2c-sso-ief/tree/master/onboarding_tutorials/AzureB2C)


## Language Support

When the Relying Party does not support a dynamic translation service such as translations.com there are the following options:

Azure B2C supports default strings for [multiple languages](https://docs.microsoft.com/en-us/azure/active-directory-b2c/language-customization?pivots=b2c-custom-policy#supported-languages) however each application (Relying Party) setup in Azure B2C must be configured to support specific languages and have custom strings localized.

The language used for the [Azure B2C Injected Content](#Azure-B2C-Injected-Content) when a page is displayed by Azure B2C is controlled by:

1. The `Accept-Language` request header as controlled by the user's browser preferences.
1. The value of the `ui_locales` parameter sent to the `authorization_endpoint` by the Relying Party can be used to force a specific language to be used.
1. The Relying Party's default language and the supported languages setup in Azure B2C.

Localization of the HTML template files can be achieved in one of two ways:

1. The HTML template files can be duplicated, translated and made available via language specific URIs such as:
   1. `/unified.<ISO language code>.html`
   1. `/<ISO language code>/unified.html`
1. The HTML template files can be hosted via a location leveraging a globalization services (e.g. MotionPoint).
   1. The globalization service must expose the localized HTML template files via a URI that inserts a valid ISO language code at a **consistent location**.
      1. Note: this will likely require additional work to support English at a location using the "en" ISO language code.
   
Where you have chosen not to host an HTML template file and instead rely on Azure B2C using a simple CenturyLink branded default UI for any files that are needed (e.g. `exception.html`) localization options will be more limited.

Manual language switching based on a user action can be achieved in one of two ways:

1. A switch in the Relying Party that results in a specific `ui_locales` parameter being sent to the `authorization_endpoint`.
   1. This happens **before** the login page is shown.
2. A JavaScript switch in the `unified.html` template file that causes an addition of, or change to, the `ui_locales` parameter in the current `window.location` value.
   1. This happens **whilst** the login page is being shown.

   
## Appendices

### CORS

The HTML template files need to be hosted from a CORS enabled internet accessible location.

Multiple mechanisms to insert http headers are available but, as an example, to enable CORS for the /azure location on an nginx server using the optional [more_set_headers](https://github.com/openresty/headers-more-nginx-module#more_set_headers) directive use:

        location /azure {
            more_set_headers "Access-Control-Allow-Origin: *;
            more_set_headers "Access-Control-Allow-Methods: GET, PUT, OPTIONS";
            more_set_headers "Access-Control-Allow-Headers: *";
            more_set_headers "Access-Control-Expose-Headers: *";
            more_set_headers "Access-Control-Max-Age: 200";
            more_set_headers "Vary: origin";
        }

For greater security Access-Control-Allow-Origin can be set to the specific location requesting the resource (e.g. https://signin-test.centurylink.com or https://mm-signin.centurylink.com) however this does create some complications for test applications and may not suit all integrations.


### Converting existing pages for use by Azure B2C

If necessary, as an alternative to using a sample page, an existing Chi HTML file can be converted for use by Azure B2C. The basics of converting an existing Chi HTML file into something that can be used by Azure B2C is:

1. Wrap the contents of the `body` element in a `div` with a class name of "chi" (because `html` and `body` tags get rewritten by Azure B2C).
1. Insert a `div` with an id of "api" at the position in the document where the content created by Azure B2C should be injected.
1. Insert the default common and page specific Azure B2C styles in the head element **before** Chi styles (see `ssi/azure_styles.html` and the "page specific azure b2c styles" comment in the head element of each sample page)
1. Insert additional styles **after** Chi styles (see the "azure additions for chi" comment in `ssi/head_links.html`)
1. Make all links absolute.
1. Do **not** link to jQuery (Azure B2C loads it automatically).
1. Add `data-preload="true"` on linked style sheets to avoid page flicker.
1. Add JavaScript to insert Chi class names into elements inserted into the `div#api` element. Adopt a minimal change principal here.
1. Add JavaScript as required to substitute error messages based on the error code
   1. This allows the error message to contain HTML elements (which are not allowed in Azure B2C strings)
1. Add JavaScript as required to insert any custom business logic (e.g. append domain to username)
1. If any Chi JavaScript functions are required (e.g. chi.dropdown()) they **must** be inserted using a mechanism that handles potentially slow loading via Azure B2C.
   1. Look for `chi.dropdown` in `unified.html` for an example
   1. A similar approach can be taken for any other JavaScript functions loaded externally that are slow to load in Azure B2C (e.g. https://assets.adobedtm.com, https://www.onelink-edge.com etc.)


### Additional Links

* Details regarding Azure B2C UI customization can be found at https://docs.microsoft.com/en-us/azure/active-directory-b2c/customize-ui-with-html?pivots=b2c-custom-policy
* Details regarding Azure B2C language customization and localization can be found at https://docs.microsoft.com/en-us/azure/active-directory-b2c/language-customization?pivots=b2c-custom-policy
* Azure B2C onboarding tutorial is available at https://github.com/CenturyLink/b2c-sso-ief/tree/master/onboarding_tutorials/AzureB2C
