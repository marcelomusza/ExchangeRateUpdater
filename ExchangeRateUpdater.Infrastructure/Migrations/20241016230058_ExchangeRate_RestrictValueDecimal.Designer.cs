﻿// <auto-generated />
using System;
using ExchangeRateUpdater.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExchangeRateUpdater.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241016230058_ExchangeRate_RestrictValueDecimal")]
    partial class ExchangeRate_RestrictValueDecimal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ExchangeRateUpdater.Domain.Model.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("ExchangeRateUpdater.Domain.Model.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "AED"
                        },
                        new
                        {
                            Id = 2,
                            Code = "AFN"
                        },
                        new
                        {
                            Id = 3,
                            Code = "ALL"
                        },
                        new
                        {
                            Id = 4,
                            Code = "AMD"
                        },
                        new
                        {
                            Id = 5,
                            Code = "ANG"
                        },
                        new
                        {
                            Id = 6,
                            Code = "AOA"
                        },
                        new
                        {
                            Id = 7,
                            Code = "ARS"
                        },
                        new
                        {
                            Id = 8,
                            Code = "AUD"
                        },
                        new
                        {
                            Id = 9,
                            Code = "AWG"
                        },
                        new
                        {
                            Id = 10,
                            Code = "AZN"
                        },
                        new
                        {
                            Id = 11,
                            Code = "BAM"
                        },
                        new
                        {
                            Id = 12,
                            Code = "BBD"
                        },
                        new
                        {
                            Id = 13,
                            Code = "BDT"
                        },
                        new
                        {
                            Id = 14,
                            Code = "BGN"
                        },
                        new
                        {
                            Id = 15,
                            Code = "BHD"
                        },
                        new
                        {
                            Id = 16,
                            Code = "BIF"
                        },
                        new
                        {
                            Id = 17,
                            Code = "BMD"
                        },
                        new
                        {
                            Id = 18,
                            Code = "BND"
                        },
                        new
                        {
                            Id = 19,
                            Code = "BOB"
                        },
                        new
                        {
                            Id = 20,
                            Code = "BOV"
                        },
                        new
                        {
                            Id = 21,
                            Code = "BRL"
                        },
                        new
                        {
                            Id = 22,
                            Code = "BSD"
                        },
                        new
                        {
                            Id = 23,
                            Code = "BTN"
                        },
                        new
                        {
                            Id = 24,
                            Code = "BWP"
                        },
                        new
                        {
                            Id = 25,
                            Code = "BYN"
                        },
                        new
                        {
                            Id = 26,
                            Code = "BZD"
                        },
                        new
                        {
                            Id = 27,
                            Code = "CAD"
                        },
                        new
                        {
                            Id = 28,
                            Code = "CDF"
                        },
                        new
                        {
                            Id = 29,
                            Code = "CHE"
                        },
                        new
                        {
                            Id = 30,
                            Code = "CHF"
                        },
                        new
                        {
                            Id = 31,
                            Code = "CHW"
                        },
                        new
                        {
                            Id = 32,
                            Code = "CLF"
                        },
                        new
                        {
                            Id = 33,
                            Code = "CLP"
                        },
                        new
                        {
                            Id = 34,
                            Code = "CNY"
                        },
                        new
                        {
                            Id = 35,
                            Code = "COP"
                        },
                        new
                        {
                            Id = 36,
                            Code = "COU"
                        },
                        new
                        {
                            Id = 37,
                            Code = "CRC"
                        },
                        new
                        {
                            Id = 38,
                            Code = "CUP"
                        },
                        new
                        {
                            Id = 39,
                            Code = "CVE"
                        },
                        new
                        {
                            Id = 40,
                            Code = "CZK"
                        },
                        new
                        {
                            Id = 41,
                            Code = "DJF"
                        },
                        new
                        {
                            Id = 42,
                            Code = "DKK"
                        },
                        new
                        {
                            Id = 43,
                            Code = "DOP"
                        },
                        new
                        {
                            Id = 44,
                            Code = "DZD"
                        },
                        new
                        {
                            Id = 45,
                            Code = "EGP"
                        },
                        new
                        {
                            Id = 46,
                            Code = "ERN"
                        },
                        new
                        {
                            Id = 47,
                            Code = "ETB"
                        },
                        new
                        {
                            Id = 48,
                            Code = "EUR"
                        },
                        new
                        {
                            Id = 49,
                            Code = "FJD"
                        },
                        new
                        {
                            Id = 50,
                            Code = "FKP"
                        },
                        new
                        {
                            Id = 51,
                            Code = "GBP"
                        },
                        new
                        {
                            Id = 52,
                            Code = "GEL"
                        },
                        new
                        {
                            Id = 53,
                            Code = "GHS"
                        },
                        new
                        {
                            Id = 54,
                            Code = "GIP"
                        },
                        new
                        {
                            Id = 55,
                            Code = "GMD"
                        },
                        new
                        {
                            Id = 56,
                            Code = "GNF"
                        },
                        new
                        {
                            Id = 57,
                            Code = "GTQ"
                        },
                        new
                        {
                            Id = 58,
                            Code = "GYD"
                        },
                        new
                        {
                            Id = 59,
                            Code = "HKD"
                        },
                        new
                        {
                            Id = 60,
                            Code = "HNL"
                        },
                        new
                        {
                            Id = 61,
                            Code = "HRK"
                        },
                        new
                        {
                            Id = 62,
                            Code = "HTG"
                        },
                        new
                        {
                            Id = 63,
                            Code = "HUF"
                        },
                        new
                        {
                            Id = 64,
                            Code = "IDR"
                        },
                        new
                        {
                            Id = 65,
                            Code = "ILS"
                        },
                        new
                        {
                            Id = 66,
                            Code = "INR"
                        },
                        new
                        {
                            Id = 67,
                            Code = "IQD"
                        },
                        new
                        {
                            Id = 68,
                            Code = "IRR"
                        },
                        new
                        {
                            Id = 69,
                            Code = "ISK"
                        },
                        new
                        {
                            Id = 70,
                            Code = "JMD"
                        },
                        new
                        {
                            Id = 71,
                            Code = "JOD"
                        },
                        new
                        {
                            Id = 72,
                            Code = "JPY"
                        },
                        new
                        {
                            Id = 73,
                            Code = "KES"
                        },
                        new
                        {
                            Id = 74,
                            Code = "KGS"
                        },
                        new
                        {
                            Id = 75,
                            Code = "KHR"
                        },
                        new
                        {
                            Id = 76,
                            Code = "KMF"
                        },
                        new
                        {
                            Id = 77,
                            Code = "KPW"
                        },
                        new
                        {
                            Id = 78,
                            Code = "KRW"
                        },
                        new
                        {
                            Id = 79,
                            Code = "KWD"
                        },
                        new
                        {
                            Id = 80,
                            Code = "KYD"
                        },
                        new
                        {
                            Id = 81,
                            Code = "KZT"
                        },
                        new
                        {
                            Id = 82,
                            Code = "LAK"
                        },
                        new
                        {
                            Id = 83,
                            Code = "LBP"
                        },
                        new
                        {
                            Id = 84,
                            Code = "LKR"
                        },
                        new
                        {
                            Id = 85,
                            Code = "LRD"
                        },
                        new
                        {
                            Id = 86,
                            Code = "LSL"
                        },
                        new
                        {
                            Id = 87,
                            Code = "LYD"
                        },
                        new
                        {
                            Id = 88,
                            Code = "MAD"
                        },
                        new
                        {
                            Id = 89,
                            Code = "MDL"
                        },
                        new
                        {
                            Id = 90,
                            Code = "MGA"
                        },
                        new
                        {
                            Id = 91,
                            Code = "MKD"
                        },
                        new
                        {
                            Id = 92,
                            Code = "MMK"
                        },
                        new
                        {
                            Id = 93,
                            Code = "MNT"
                        },
                        new
                        {
                            Id = 94,
                            Code = "MOP"
                        },
                        new
                        {
                            Id = 95,
                            Code = "MRU"
                        },
                        new
                        {
                            Id = 96,
                            Code = "MUR"
                        },
                        new
                        {
                            Id = 97,
                            Code = "MVR"
                        },
                        new
                        {
                            Id = 98,
                            Code = "MWK"
                        },
                        new
                        {
                            Id = 99,
                            Code = "MXN"
                        },
                        new
                        {
                            Id = 100,
                            Code = "MXV"
                        },
                        new
                        {
                            Id = 101,
                            Code = "MYR"
                        },
                        new
                        {
                            Id = 102,
                            Code = "MZN"
                        },
                        new
                        {
                            Id = 103,
                            Code = "NAD"
                        },
                        new
                        {
                            Id = 104,
                            Code = "NGN"
                        },
                        new
                        {
                            Id = 105,
                            Code = "NIO"
                        },
                        new
                        {
                            Id = 106,
                            Code = "NOK"
                        },
                        new
                        {
                            Id = 107,
                            Code = "NPR"
                        },
                        new
                        {
                            Id = 108,
                            Code = "NZD"
                        },
                        new
                        {
                            Id = 109,
                            Code = "OMR"
                        },
                        new
                        {
                            Id = 110,
                            Code = "PAB"
                        },
                        new
                        {
                            Id = 111,
                            Code = "PEN"
                        },
                        new
                        {
                            Id = 112,
                            Code = "PGK"
                        },
                        new
                        {
                            Id = 113,
                            Code = "PHP"
                        },
                        new
                        {
                            Id = 114,
                            Code = "PKR"
                        },
                        new
                        {
                            Id = 115,
                            Code = "PLN"
                        },
                        new
                        {
                            Id = 116,
                            Code = "PYG"
                        },
                        new
                        {
                            Id = 117,
                            Code = "QAR"
                        },
                        new
                        {
                            Id = 118,
                            Code = "RON"
                        },
                        new
                        {
                            Id = 119,
                            Code = "RSD"
                        },
                        new
                        {
                            Id = 120,
                            Code = "RUB"
                        },
                        new
                        {
                            Id = 121,
                            Code = "RWF"
                        },
                        new
                        {
                            Id = 122,
                            Code = "SAR"
                        },
                        new
                        {
                            Id = 123,
                            Code = "SBD"
                        },
                        new
                        {
                            Id = 124,
                            Code = "SCR"
                        },
                        new
                        {
                            Id = 125,
                            Code = "SDG"
                        },
                        new
                        {
                            Id = 126,
                            Code = "SEK"
                        },
                        new
                        {
                            Id = 127,
                            Code = "SGD"
                        },
                        new
                        {
                            Id = 128,
                            Code = "SHP"
                        },
                        new
                        {
                            Id = 129,
                            Code = "SLE"
                        },
                        new
                        {
                            Id = 130,
                            Code = "SOS"
                        },
                        new
                        {
                            Id = 131,
                            Code = "SRD"
                        },
                        new
                        {
                            Id = 132,
                            Code = "SSP"
                        },
                        new
                        {
                            Id = 133,
                            Code = "STN"
                        },
                        new
                        {
                            Id = 134,
                            Code = "SVC"
                        },
                        new
                        {
                            Id = 135,
                            Code = "SYP"
                        },
                        new
                        {
                            Id = 136,
                            Code = "SZL"
                        },
                        new
                        {
                            Id = 137,
                            Code = "THB"
                        },
                        new
                        {
                            Id = 138,
                            Code = "TJS"
                        },
                        new
                        {
                            Id = 139,
                            Code = "TMT"
                        },
                        new
                        {
                            Id = 140,
                            Code = "TND"
                        },
                        new
                        {
                            Id = 141,
                            Code = "TOP"
                        },
                        new
                        {
                            Id = 142,
                            Code = "TRY"
                        },
                        new
                        {
                            Id = 143,
                            Code = "TTD"
                        },
                        new
                        {
                            Id = 144,
                            Code = "TWD"
                        },
                        new
                        {
                            Id = 145,
                            Code = "TZS"
                        },
                        new
                        {
                            Id = 146,
                            Code = "UAH"
                        },
                        new
                        {
                            Id = 147,
                            Code = "UGX"
                        },
                        new
                        {
                            Id = 148,
                            Code = "USD"
                        },
                        new
                        {
                            Id = 149,
                            Code = "USN"
                        },
                        new
                        {
                            Id = 150,
                            Code = "UYI"
                        },
                        new
                        {
                            Id = 151,
                            Code = "UYU"
                        },
                        new
                        {
                            Id = 152,
                            Code = "UYW"
                        },
                        new
                        {
                            Id = 153,
                            Code = "UZS"
                        },
                        new
                        {
                            Id = 154,
                            Code = "VED"
                        },
                        new
                        {
                            Id = 155,
                            Code = "VES"
                        },
                        new
                        {
                            Id = 156,
                            Code = "VND"
                        },
                        new
                        {
                            Id = 157,
                            Code = "VUV"
                        },
                        new
                        {
                            Id = 158,
                            Code = "WST"
                        },
                        new
                        {
                            Id = 159,
                            Code = "XAF"
                        },
                        new
                        {
                            Id = 160,
                            Code = "XAG"
                        },
                        new
                        {
                            Id = 161,
                            Code = "XAU"
                        },
                        new
                        {
                            Id = 162,
                            Code = "XBA"
                        },
                        new
                        {
                            Id = 163,
                            Code = "XBB"
                        },
                        new
                        {
                            Id = 164,
                            Code = "XBC"
                        },
                        new
                        {
                            Id = 165,
                            Code = "XBD"
                        },
                        new
                        {
                            Id = 166,
                            Code = "XCD"
                        },
                        new
                        {
                            Id = 167,
                            Code = "XDR"
                        },
                        new
                        {
                            Id = 168,
                            Code = "XOF"
                        },
                        new
                        {
                            Id = 169,
                            Code = "XPD"
                        },
                        new
                        {
                            Id = 170,
                            Code = "XPF"
                        },
                        new
                        {
                            Id = 171,
                            Code = "XPT"
                        },
                        new
                        {
                            Id = 172,
                            Code = "XSU"
                        },
                        new
                        {
                            Id = 173,
                            Code = "XTS"
                        },
                        new
                        {
                            Id = 174,
                            Code = "XUA"
                        },
                        new
                        {
                            Id = 175,
                            Code = "XXX"
                        },
                        new
                        {
                            Id = 176,
                            Code = "YER"
                        },
                        new
                        {
                            Id = 177,
                            Code = "ZAR"
                        },
                        new
                        {
                            Id = 178,
                            Code = "ZMW"
                        },
                        new
                        {
                            Id = 179,
                            Code = "ZWG"
                        });
                });

            modelBuilder.Entity("ExchangeRateUpdater.Domain.Model.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SourceCurrencyId")
                        .HasColumnType("int")
                        .HasColumnName("SourceCurrency");

                    b.Property<int>("TargetCurrencyId")
                        .HasColumnType("int")
                        .HasColumnName("TargetCurrency");

                    b.Property<decimal>("Value")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("SourceCurrencyId");

                    b.HasIndex("TargetCurrencyId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("ExchangeRateUpdater.Domain.Model.ExchangeRate", b =>
                {
                    b.HasOne("ExchangeRateUpdater.Domain.Model.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExchangeRateUpdater.Domain.Model.Currency", "SourceCurrency")
                        .WithMany()
                        .HasForeignKey("SourceCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExchangeRateUpdater.Domain.Model.Currency", "TargetCurrency")
                        .WithMany()
                        .HasForeignKey("TargetCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("SourceCurrency");

                    b.Navigation("TargetCurrency");
                });
#pragma warning restore 612, 618
        }
    }
}
