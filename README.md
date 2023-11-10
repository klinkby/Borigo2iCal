# Borigo2iCal

A gateway function to convert a Borigo facility's bookings to iCal calendar format for 
integration in popular calendar apps.


## Usage

After deployment the app will expose the endpoint: `https://<YOURSITE>.azurewebsites.net/api/bookings?date=2023-11-10`

The date parameter is optional and defaults to today's date minus 4 days.
The function returns a VCalendar file with the bookings from the specified date.

## Hosting

Hosted on Azure Functions v4 (Linux) Consumption Plan, the function is triggered by a HTTP request.

Requires the following App Settings in the Function App:

- `REMEMBER_USER_TOKEN`: The cookie secret value used to authenticate the user with Borigo
- `SUBDOMAIN`: The subdomain of the Borigo account *(e.g. `https://<SUBDOMAIN>.borigo.dk`)*
- `FACILITY_ID`: The ID of the facility to fetch bookings from *(e.g. `/booking-engine/bookings?facility_id=<FACILITY_ID>`)*


## References

- [dotnet 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Azure Functions v4](https://docs.microsoft.com/en-us/azure/azure-functions/functions-versions)
- Nuget [RestSharp](https://github.com/restsharp/RestSharp) for communication with the Borigo REST API
- Nuget [Klinkby.VCard](https://github.com/klinkby/klinkby.vcard) for serializing to VCalendar format


## License

[MIT Licensed](LICENSE).

