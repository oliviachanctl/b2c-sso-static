# Custom UI Examples

## Enterprise Sample

The files in ./azure/enterprise_sample provide a sample Chi based UI for Enterprise. Refer to ./azure/enterprise_sample/README.md for more details

## Chi

The files in ./azure/chi provide the starting point for a Chi based UI and is used for development and when creating samples (see above). Refer to ./azure/chi/README.md for more details

## Lumen

The files in ./azure/lumen provide a Lumen branded version of the Azure Ocean Blue sample UI. Refer to ./azure/lumen/README.md for more details

## Ocean Blue

The files in the three ./azure/ocean_blue* directories archive are the different Azure Ocean Blue sample UIs. Refer to the README.md in each directory for more details

## Assets

The files in ./azure/assets a support all the Custom UI options to different extents except Chi. Not all assets are used by all Custom UIs


# CORS

The files here need to be hosted from a CORS enabled internet accessible location.

To enable CORS using nginx for the directory /azure:

        location /azure {
            more_set_headers "Access-Control-Allow-Origin: https://centurylinkb2ctest.b2clogin.com";
            more_set_headers "Access-Control-Allow-Methods: GET, PUT, OPTIONS";
            more_set_headers "Access-Control-Allow-Headers: *";
            more_set_headers "Access-Control-Expose-Headers: *";
            more_set_headers "Access-Control-Max-Age: 200";
            more_set_headers "Vary: origin";
        }
