﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

namespace Test.Chapter08Listings.EfClasses
{
    public class PaymentCard : Payment
    {
        public string ReceiptCode { get; set; }
    }
}