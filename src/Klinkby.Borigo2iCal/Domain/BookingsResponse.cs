﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Klinkby.Borigo2iCal.Domain;

// <auto-generated /> by https://app.quicktype.io/ 
//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using Klinkby.Borigo2iCal;
//
//    var welcome = Welcome.FromJson(jsonString);
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

public record BookingsResponse
{
    [JsonPropertyName("bookings")] public Booking[] Bookings { get; set; }

    [JsonPropertyName("offset")] public long Offset { get; set; }

    [JsonPropertyName("facility")] public Facility Facility { get; set; }
}

public record Booking
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("starts_at")] public DateTimeOffset StartsAt { get; set; }

    [JsonPropertyName("ends_at")] public DateTimeOffset EndsAt { get; set; }

    [JsonPropertyName("booking_type")] public string BookingType { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("price")] public long Price { get; set; }

    [JsonPropertyName("deposit")] public long Deposit { get; set; }

    [JsonPropertyName("payment_method")] public string PaymentMethod { get; set; }

    [JsonPropertyName("customer_name")] public string CustomerName { get; set; }

    [JsonPropertyName("facility_id")] public long FacilityId { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("outstanding_due_on")]
    public DateTimeOffset OutstandingDueOn { get; set; }

    [JsonPropertyName("refund_deposit_at")]
    public DateTimeOffset RefundDepositAt { get; set; }

    [JsonPropertyName("cancelled_at")] public DateTimeOffset? CancelledAt { get; set; }

    [JsonPropertyName("default_cancellation_fee_until")]
    public object DefaultCancellationFeeUntil { get; set; }

    [JsonPropertyName("default_cancellation_fee")]
    public long DefaultCancellationFee { get; set; }

    [JsonPropertyName("late_cancellation_fee")]
    public long LateCancellationFee { get; set; }

    [JsonPropertyName("cancellation_window_unit")]
    public object CancellationWindowUnit { get; set; }

    [JsonPropertyName("cancellation_window_value")]
    public object CancellationWindowValue { get; set; }

    [JsonPropertyName("automation")] public bool Automation { get; set; }

    [JsonPropertyName("phone")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Phone { get; set; }

    [JsonPropertyName("unit")] public Unit Unit { get; set; }

    [JsonPropertyName("responsible")] public Responsible Responsible { get; set; }
}

public record Responsible
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("initials")] public string Initials { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("path")] public string Path { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("profile_image")]
    public ProfileImage ProfileImage { get; set; }

    [JsonPropertyName("address")] public Address Address { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }
}

public record Address
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }
}

public record ProfileImage
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("aspect_ratio")] public double AspectRatio { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("url_128x-")] public Uri Url128X { get; set; }

    [JsonPropertyName("url_256x-")] public Uri Url256X { get; set; }

    [JsonPropertyName("url_512x-")] public Uri Url512X { get; set; }

    [JsonPropertyName("url_1024x-")] public Uri Url1024X { get; set; }

    [JsonPropertyName("url_1280x-")] public Uri Url1280X { get; set; }

    [JsonPropertyName("url_2048x-")] public Uri Url2048X { get; set; }

    [JsonPropertyName("url_1024x1024")] public Uri Url1024X1024 { get; set; }

    [JsonPropertyName("url_720x720")] public Uri Url720X720 { get; set; }

    [JsonPropertyName("url_600x600")] public Uri Url600X600 { get; set; }

    [JsonPropertyName("url_480x480")] public Uri Url480X480 { get; set; }

    [JsonPropertyName("url_300x300")] public Uri Url300X300 { get; set; }

    [JsonPropertyName("url_192x192")] public Uri Url192X192 { get; set; }

    [JsonPropertyName("url_96x96")] public Uri Url96X96 { get; set; }

    [JsonPropertyName("url_72x72")] public Uri Url72X72 { get; set; }

    [JsonPropertyName("url_48x48")] public Uri Url48X48 { get; set; }

    [JsonPropertyName("url_50x50")] public Uri Url50X50 { get; set; }

    [JsonPropertyName("url_32x32")] public Uri Url32X32 { get; set; }

    [JsonPropertyName("url_25x25")] public Uri Url25X25 { get; set; }

    [JsonPropertyName("url_large")] public Uri UrlLarge { get; set; }

    [JsonPropertyName("imageable_type")] public string ImageableType { get; set; }

    [JsonPropertyName("imageable_id")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long ImageableId { get; set; }
}

public record Unit
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("community_id")] public long CommunityId { get; set; }

    [JsonPropertyName("facility_id")] public long FacilityId { get; set; }

    [JsonPropertyName("position")] public long Position { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("default")] public bool Default { get; set; }

    [JsonPropertyName("includes_all_units")]
    public bool IncludesAllUnits { get; set; }

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
}

public record Facility
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("community_id")] public long CommunityId { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }

    [JsonPropertyName("needs_booking_approval")]
    public bool NeedsBookingApproval { get; set; }

    [JsonPropertyName("accepts_payment")] public bool AcceptsPayment { get; set; }

    [JsonPropertyName("terms_and_conditions_url")]
    public object TermsAndConditionsUrl { get; set; }

    [JsonPropertyName("cancellation_window_unit")]
    public object CancellationWindowUnit { get; set; }

    [JsonPropertyName("cancellation_window_value")]
    public object CancellationWindowValue { get; set; }

    [JsonPropertyName("highlighted_terms_list")]
    public object[] HighlightedTermsList { get; set; }

    [JsonPropertyName("first_rate")] public double FirstRate { get; set; }

    [JsonPropertyName("deposit")] public PurpleDeposit Deposit { get; set; }

    [JsonPropertyName("deposits")] public DepositElement[] Deposits { get; set; }

    [JsonPropertyName("path")] public string Path { get; set; }

    [JsonPropertyName("is_current_person_booking_admin")]
    public bool IsCurrentPersonBookingAdmin { get; set; }
}

public record PurpleDeposit
{
}

public record DepositElement
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("community_id")] public long CommunityId { get; set; }

    [JsonPropertyName("facility_id")] public long FacilityId { get; set; }

    [JsonPropertyName("booking_type")] public string BookingType { get; set; }

    [JsonPropertyName("criteria")] public string Criteria { get; set; }

    [JsonPropertyName("amount")] public long Amount { get; set; }

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
}

internal class ParseStringConverter : JsonConverter<long>
{
    public static readonly ParseStringConverter Singleton = new();

    public override bool CanConvert(Type t)
    {
        return t == typeof(long);
    }

    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        long l;
        if (long.TryParse(value, out l)) return l;
        throw new Exception("Cannot unmarshal type long");
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToString(), options);
    }
}

#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603