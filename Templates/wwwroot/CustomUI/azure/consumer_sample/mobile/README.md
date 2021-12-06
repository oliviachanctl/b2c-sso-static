# Consumer Sample UI > JSON > Mobile

Consmobile requires two files to facilitate password synchronization between mobile app and browser site:

* assetlinks.json will be made available via https://<Consumer_Hostname>/.well-known/assetlinks.json
* appleAppSiteAssociation.json will be made available via https://<Consumer_Hostname>/.well-known/apple-app-site-association
 
Both must be placed in an internet accessible CORS enabled location. The location of the deployed files and any environment specific or localized variants **must** be provided to the Azure B2C Team in order for your application to be correctly configured.

Note that Azure FrontDoors is **NOT** able to proxy a request for /.well-known/apple-app-site-association **and** also rename the request filename to appleAppSiteAssociation.json. Therefore a rewrite rule must be added to the server hosting the files:

RewriteRule ^/static/json/mobile/apple-app-site-association$ /static/json/mobile/appleAppSiteAssociation.json [L,NC,PT]