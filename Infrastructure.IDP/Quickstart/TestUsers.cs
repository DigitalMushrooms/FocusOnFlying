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
                            new Claim(JwtClaimTypes.NickName, "fwojciechowska"),
                            new Claim(JwtClaimTypes.Name, "Franciszka Wojciechowska"),
                            new Claim(JwtClaimTypes.GivenName, "Franciszka"),
                            new Claim(JwtClaimTypes.FamilyName, "Wojciechowska"),
                            new Claim(JwtClaimTypes.Email, "franciszkawojciechowska@email.com"),
                            new Claim("uprawnienia", "VLOS"),
                            new Claim("uprawnienia", "BVLOS"),
                            new Claim(JwtClaimTypes.Role, "USLUGA_TWORZENIE_EDYCJA"),
                            new Claim(JwtClaimTypes.Role, "USLUGA_PODGLAD"),
                            new Claim(JwtClaimTypes.Role, "DRON_TWORZENIE_EDYCJA"),
                            new Claim(JwtClaimTypes.Role, "DRON_PODGLAD"),
                            new Claim(JwtClaimTypes.Role, "PRACOWNIK_TWORZENIE_EDYCJA"),
                            new Claim(JwtClaimTypes.Role, "KLIENT_TWORZENIE_EDYCJA"),
                            new Claim(JwtClaimTypes.Role, "KLIENT_PODGLAD"),
                            new Claim(JwtClaimTypes.Role, "RAPORT_TWORZENIE_EDYCJA"),
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "tsobczak",
                        Password = "12345",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.NickName, "tsobczak"),
                            new Claim(JwtClaimTypes.Name, "Tymoteusz Sobczak"),
                            new Claim(JwtClaimTypes.GivenName, "Tymoteusz"),
                            new Claim(JwtClaimTypes.FamilyName, "Sobczak"),
                            new Claim(JwtClaimTypes.Email, "tymoteuszsobczak@email.com"),
                            new Claim(JwtClaimTypes.Role, "USLUGA_TWORZENIE_EDYCJA"),
                            new Claim(JwtClaimTypes.Role, "USLUGA_PODGLAD"),
                            new Claim("uprawnienia", "VLOS")
                        }
                    }
                };
            }
        }
    }
}