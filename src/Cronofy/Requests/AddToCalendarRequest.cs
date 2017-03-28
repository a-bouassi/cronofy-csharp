﻿namespace Cronofy.Requests
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an add to calendar request.
    /// </summary>
    public sealed class AddToCalendarRequest
    {
        /// <summary>
        /// Gets or sets the client id for the request.
        /// </summary>
        /// <value>
        /// The client id for the request.
        /// </value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret for the request.
        /// </summary>
        /// <value>
        /// The client secret for the request.
        /// </value>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the oauth details for the request.
        /// </summary>
        /// <value>
        /// The oauth details for the request.
        /// </value>
        [JsonProperty("oauth")]
        public OAuthDetails OAuth { get; set; }

        /// <summary>
        /// Gets or sets the event details for the request.
        /// </summary>
        /// <value>
        /// The event details for the request.
        /// </value>
        [JsonProperty("event")]
        public UpsertEventRequest Event { get; set; }

        /// <summary>
        /// Class for the serialization of the oauth details.
        /// </summary>
        public sealed class OAuthDetails 
        {
            /// <summary>
            /// Gets or sets the oauth redirect url.
            /// </summary>
            /// <value>
            /// The redirect url for the oauth flow.
            /// </value>
            [JsonProperty("redirect_url")]
            public string RedirectUrl { get; set; }

            /// <summary>
            /// Gets or sets the oauth scopes.
            /// </summary>
            /// <value>
            /// The scopes of the oauth flow.
            /// </value>
            [JsonProperty("scope")]
            public string Scope { get; set; }

            /// <summary>
            /// Gets or sets the oauth state.
            /// </summary>
            /// <value>
            /// The state for the oauth flow.
            /// </value>
            [JsonProperty("state")]
            public string State { get; set; }
        }
    }
}
