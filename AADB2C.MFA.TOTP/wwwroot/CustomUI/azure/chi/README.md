# Lumen + Chi

These files provide a [Chi](https://lib.lumen.com/chi/) based Lumen branded UI. They are used for development and when creating samples.

They are originally based on the default Azure Ocean Blue templates at CustomUI/azure/ocean_blue_default and support the latest [Page Layouts](https://docs.microsoft.com/en-us/azure/active-directory-b2c/page-layout) used in [Content Definitions](https://docs.microsoft.com/en-us/azure/active-directory-b2c/contentdefinitions)

All Azure b2c CSS is inline to reduce http request traffic. The CSS could be separated (like CustomUI/azure/ocean_blue_default_separated) if required.

To reduce duplication [nginx ssi](http://nginx.org/en/docs/http/ngx_http_ssi_module.html) (server side includes) are used to insert content from CustomUI/chi/ssi.

JavaScript is used to add all necessary Chi CSS class names to the Azure b2c injected content. This avoids excessive editing of Azure CSS to look more like Chi. However, it is unlikely that all possible Azure injected content has been included during the Chi class name insertions.


The basics of converting an existing Chi HTML file into something that can be used by Azure B2C is:

1. Wrap the contents of the body element in a div with a class name of "chi" (because html and body tags get rewritten by Azure B2C).
1. Insert a div with an id of "api" at the position in the document where the content created by Azure B2C will be injected.
1. Make all links absolute.
1. Do not link to jQuery (Azure B2C loads it).
1. Use server side includes to avoid duplication of common elements (if pages are not dynamically created).
1. Add data-preload="true" on linked style sheets to avoid page flicker.
1. Use JS to insert Chi class names into elements inserted into the div#api element. Adopt a minimal change principal here.
1. Use JS as required to substitute error messages based on the error code
   1. This allows the error message to contain HTML elements (which are not allowed in Azure B2C strings)
1. Use JS as required to insert any custom business logic (e.g. append domain to username)
1. If any Chi JS functions are required (e.g. chi.dropdown()) they **must** be inserted using a mechanism that handles potentially slow loading via Azure B2C. 
   1. Look for `chi.dropdown` in `unified.html` for an example
