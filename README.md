# Borigo2iCal

A gateway function to convert a Borigo facility's bookings to iCal calendar format for 
integration in popular calendar apps.

## Hosting

Hosted on Azure Functions (Linux) Consumption Plan, the function is triggered by a HTTP request.

Requires the following App Settings in the Function App:

- `REMEMBER_USER_TOKEN`: The cookie secret value used to authenticate the user with Borigo
- `SUBDOMAIN`: The subdomain of the Borigo account *(e.g. `https://<SUBDOMAIN>.borigo.dk`)*
- `FACILITY_ID`: The ID of the facility to fetch bookings from *(e.g. `/booking-engine/bookings?facility_id=<FACILITY_ID>`)*

## References

- [RestSharp](https://github.com/restsharp/RestSharp) for communication with the Borigo API
- [Klinkby.VCard](https://github.com/klinkby/klinkby.vcard) for serializing to VCalendar format

