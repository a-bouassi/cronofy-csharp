﻿using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class GetResources : Base
    {
        [Test]
        public void CanGetResources()
        {
            Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/resources")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .ResponseCode(200)
                .ResponseBody(
                    @"{
  ""resources"": [
    {
      ""email"": ""resource_one@cronofy.com"",
      ""name"": ""Resource One""
    },
    {
      ""email"": ""resource_two@cronofy.com"",
      ""name"": ""Resource Two""
    },
    {
      ""email"": ""resource_three@cronofy.com"",
      ""name"": ""Resource Three""
    }
  ]
}")
        );

            var resources = Client.GetResources();

            CollectionAssert.AreEqual(
                new List<Resource> {
                    new Resource {
                        Email = "resource_one@cronofy.com",
                        Name = "Resource One",
                    },
                    new Resource {
                        Email = "resource_two@cronofy.com",
                        Name = "Resource Two",
                    },
                    new Resource {
                        Email = "resource_three@cronofy.com",
                        Name = "Resource Three",
                    },
                },
                resources.ToList());
        }
    }
}
