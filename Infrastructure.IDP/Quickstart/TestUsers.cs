// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {                
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "1",
                        Username = "fwojciechowska",
                        Password = "12345",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Franciszka Wojciechowska"),
                            new Claim(JwtClaimTypes.GivenName, "Franciszka"),
                            new Claim(JwtClaimTypes.FamilyName, "Wojciechowska"),
                            new Claim(JwtClaimTypes.Email, "FranciszkaWojciechowska@armyspy.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim("uprawnienia", "VLOS"),
                            new Claim("uprawnienia", "BVLOS"),
                            new Claim(JwtClaimTypes.Role, "Kierownik"),
                            new Claim(JwtClaimTypes.Role, "Pracownik")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "bob",
                        Password = "12345",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim("uprawnienia", "VLOS"),
                            new Claim(JwtClaimTypes.Role, "Pracownik")
                        }
                    }
                };
            }
        }
    }
}