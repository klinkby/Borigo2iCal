# Borigo2iCal

A gateway function to convert a ~~Borigo~~ HeyNabo! facility's bookings to iCal calendar format for 
integration in popular calendar apps.


## Usage

After deployment the app will expose the endpoint: `https://<YOURSITE>.azurewebsites.net/api/bookings/{facilityId}`

The facilityId parameter is required to id the HeyNabo! facility.
The function returns a VCalendar file with the bookings from the specified date.

## Hosting

Hosted on Azure Functions v4 isolated dotnet (ReadyToRun) Linux Consumption Plan, the function is triggered by a HTTP request.

Requires the following App Settings in the Function App:

- `ApiClient__RememberUserToken`: The bearer token used to authenticate the user with HeyNabo!
- `ApiClient__Subdomain`: The subdomain of the HeyNabo! account *(e.g. `https://<SUBDOMAIN>.spaces.heynabo.com`)*


## References

- [dotnet 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Azure Functions v4](https://docs.microsoft.com/en-us/azure/azure-functions/functions-versions)
- Nuget [RestSharp](https://github.com/restsharp/RestSharp) for communication with the Borigo REST API
- Nuget [Klinkby.VCard](https://github.com/klinkby/klinkby.vcard) for serializing to VCalendar format


## License

[MIT Licensed](LICENSE).

