using HaverProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using HaverProject.ViewModel;

namespace HaverProject.Data
{
    public static class HMInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            HaverContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<HaverContext>();

            try
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
              
                if (!context.Reviews.Any())
                {
                    context.Reviews.AddRange(
                        new Review
                        {

                            Id = 1,
                            Name = "Yes",

                        },
                        new Review
                        {

                            Id = 2,

                            Name = "No",

                        });

                    context.SaveChanges();
                }
                if (!context.UseAsIss.Any())
                {
                    context.UseAsIss.AddRange(
                        new UseAsIs
                        {

                            Id = 1,
                            Name = "UseAsIs",

                        },
                        new UseAsIs
                        {

                            Id = 2,

                            Name = "Repair",

                        },
                        new UseAsIs
                        {
                            Id = 3,
                            Name = "Rework",

                        },
                        new UseAsIs
                        {

                            Id = 4,
                            Name = "Scrap",

                        }
                        );
                    context.SaveChanges();
                }
                if (!context.ItemMarkeds.Any())
                {
                    context.ItemMarkeds.AddRange(
                        new ItemMarked
                        {

                            Id = 1,
                            Name = "Yes",

                        },
                        new ItemMarked
                        {

                            Id = 2,

                            Name = "No",

                        });

                    context.SaveChanges();
                }
                if (!context.SupplierOrRecInsps.Any())
                {
                    context.SupplierOrRecInsps.AddRange(
                        new SupplierOrRecInsp
                        {

                            Id = 1,
                            Name = "SupplierOrRecInsp",

                        },
                        new SupplierOrRecInsp
                        {

                            Id = 2,

                            Name = "WIP",

                        });

                    context.SaveChanges();
                }
                if (!context.SapNos.Any())
                {
                    context.SapNos.AddRange(
                         new SapNo { Id = 1, Name = "200000046" },
        new SapNo { Id = 2, Name = "200000114" },
        new SapNo { Id = 3, Name = "200000503" },
        new SapNo { Id = 4, Name = "200000541" },
        new SapNo { Id = 5, Name = "200000572" },
        new SapNo { Id = 6, Name = "200000589" },
        new SapNo { Id = 7, Name = "200000626" },
        new SapNo { Id = 8, Name = "200000879" },
        new SapNo { Id = 9, Name = "200000978" },
        new SapNo { Id = 10, Name = "200001234" },
        new SapNo { Id = 11, Name = "200001241" },
        new SapNo { Id = 12, Name = "200001586" },
        new SapNo { Id = 13, Name = "200001593" },
        new SapNo { Id = 14, Name = "200001913" },
        new SapNo { Id = 15, Name = "200001968" },
        new SapNo { Id = 16, Name = "200002040" },
        new SapNo { Id = 17, Name = "200002163" },
        new SapNo { Id = 18, Name = "200002675" },
        new SapNo { Id = 19, Name = "200002682" },
        new SapNo { Id = 20, Name = "200002699" },
        new SapNo { Id = 21, Name = "200002729" },
        new SapNo { Id = 22, Name = "200002880" },
        new SapNo { Id = 23, Name = "200002941" },
        new SapNo { Id = 24, Name = "200002996" },
        new SapNo { Id = 25, Name = "200003030" },
        new SapNo { Id = 26, Name = "200003238" },
        new SapNo { Id = 27, Name = "200003276" },
        new SapNo { Id = 28, Name = "200003368" },
        new SapNo { Id = 29, Name = "200003429" },
        new SapNo { Id = 30, Name = "200003535" },
        new SapNo { Id = 31, Name = "200003641" },
        new SapNo { Id = 32, Name = "200003672" },
        new SapNo { Id = 33, Name = "200003689" },
        new SapNo { Id = 34, Name = "200003726" },
        new SapNo { Id = 35, Name = "200003801" },
        new SapNo { Id = 36, Name = "200003979" },
        new SapNo { Id = 37, Name = "200004020" },
        new SapNo { Id = 38, Name = "200004525" },
        new SapNo { Id = 39, Name = "200005577" },
        new SapNo { Id = 40, Name = "200006314" }
    );
                
                    context.SaveChanges();
                }
            

                    if (!context.ItemProblems.Any())
                {
                    context.ItemProblems.AddRange(
                       new ItemProblem { Id = 1, Name = "Tension Bolt, ~ KS ~ 5.5 ST" },
                       new ItemProblem { Id = 2, Name = "Retarding Curtain" },
                       new ItemProblem { Id = 3, Name = "Side Plate" },
                       new ItemProblem { Id = 4, Name = "Wear Protection, PL SW 0.75 4 ~ 48 ~ ~" },
                       new ItemProblem { Id = 5, Name = "Bushing, SH 1.125 0 0.25 0.125" },
                       new ItemProblem { Id = 6, Name = "Screen Support, F 4 0 SP SE" },
                       new ItemProblem { Id = 7, Name = "Grease Retainer Collar, 900 T/F-C 1.6845" },
                       new ItemProblem { Id = 8, Name = "Guard, ~ 900 F-C DS ~ SG" },
                       new ItemProblem { Id = 9, Name = "Shaft, ~ 80 T-C 3 36 LOS 0 0" },
                       new ItemProblem { Id = 10, Name = "Balance Weight, M 900 T/F-C 137 0.5" },
                       new ItemProblem { Id = 11, Name = "Body Bracket, ~ 900 F-C ~ ~ F RM ST" },
                       new ItemProblem { Id = 12, Name = "Balance Weight, M 900 T/F-C 186.7 0.5" },
                       new ItemProblem { Id = 13, Name = "Shaft, ~ 100 F-C 5 0 LOS 0.1875 0" },
                       new ItemProblem { Id = 14, Name = "Bracket, ~ H-C SI" },
                       new ItemProblem { Id = 15, Name = "Bracket, ~ F-C RH G" },
                       new ItemProblem { Id = 16, Name = "V-Belt Pulley, 2 3V 5.3" },
                       new ItemProblem { Id = 17, Name = "Feed Box, M 8/9/1100 T/F/L-C 7 0 ST ~ ~" },
                       new ItemProblem { Id = 18, Name = "Back Plate, M 8/9/1100 T/F-C 7 0 M SD ~" },
                       new ItemProblem { Id = 19, Name = "Discharge Lip, M 8/9/1100 T/F/L-C 7 0 ST" },
                       new ItemProblem { Id = 20, Name = "Seal Ring" },
                       new ItemProblem { Id = 21, Name = "Labyrinth Ring, 100 T/F-C IL G" },
                       new ItemProblem { Id = 22, Name = "Balance Weight, M 600 T/F-C 15.2 0.0598" },
                       new ItemProblem { Id = 23, Name = "Balance Weight, M 800 T/F-C 151.6 0.5" },
                       new ItemProblem { Id = 24, Name = "Brake Unit" },
                       new ItemProblem { Id = 25, Name = "Feed Box, M 3/600 T/F/L-C 4 0 ST ~ ~" },
                       new ItemProblem { Id = 26, Name = "Pieza de Adaptación" },
                       new ItemProblem { Id = 27, Name = "Bracket, ~ F-C RH G" },
                       new ItemProblem { Id = 28, Name = "Bushing, 900 T/F-C ~ W SG 4.688 2.125" },
                       new ItemProblem { Id = 29, Name = "Balance Wheel, M 900 T/F-C 631.2 1.5 A" },
                       new ItemProblem { Id = 30, Name = "Spring Support Plate, OM 2 U" }
                        );
                    context.SaveChanges();
                }

                if (!context.Suppliers.Any())
                {
                    context.Suppliers.AddRange(
                       new Supplier { Id = 1, Name = "700285 Niagara Machine" },
        new Supplier { Id = 2, Name = "700289 Niagara Machine" },
        new Supplier { Id = 3, Name = "880065 Bickle Main Industrial Supply" },
        new Supplier { Id = 4, Name = "700397 Niagara Precision" },
        new Supplier { Id = 5, Name = "880004 Western Cast" },
        new Supplier { Id = 6, Name = "813024 Tandem" },
        new Supplier { Id = 7, Name = "813024 Precision Metalworks" },
        new Supplier { Id = 8, Name = "700147 RAF FLUID POWER" },
        new Supplier { Id = 9, Name = "700148 DEKALB FORGE COMPANY" },
        new Supplier { Id = 10, Name = "700149 CORDILLERA" },
        new Supplier { Id = 11, Name = "700150 A.W. BOHANAN COMPANY" },
        new Supplier { Id = 12, Name = "700151 SIGNAL TECHNOLOGY SYSTEMS" },
        new Supplier { Id = 13, Name = "700152 MCDA INC" },
        new Supplier { Id = 14, Name = "700153 EDWARD W DANIEL CO" },
        new Supplier { Id = 15, Name = "700155 ALCOA FASTENING SYSTEMS" },
        new Supplier { Id = 16, Name = "700157 WESTERN CAST PARTS" },
        new Supplier { Id = 17, Name = "700158 TESCO-THE ENGINEERED SALES CO." },
        new Supplier { Id = 18, Name = "700159 MCMASTER-CARR" },
        new Supplier { Id = 19, Name = "700161 DRACO SPRING MFG. CO." },
        new Supplier { Id = 20, Name = "700169 VALLEY EQUIPMENT COMPANY INC.," },
        new Supplier { Id = 21, Name = "700171 OVERLY HAUTZ MOTOR BASE CO." },
        new Supplier { Id = 22, Name = "700185 BPG/BRANDON PRODUCTS GROUP" },
        new Supplier { Id = 23, Name = "700186 CAST METALS INC.," },
        new Supplier { Id = 24, Name = "700188 TEMA ISENMANN, INC." },
        new Supplier { Id = 25, Name = "700193 VALLEY RUBBER, LLC" },
        new Supplier { Id = 26, Name = "700196 COILING TECHNOLOGIES INC" },
        new Supplier { Id = 27, Name = "700199 WARD INDUSTRIAL EQUIPMENT" },
        new Supplier { Id = 28, Name = "700203 E S FOX LTD" },
        new Supplier { Id = 29, Name = "700212 R.W. HAMILTON LTD." },
        new Supplier { Id = 30, Name = "700218 JOHN BROOKS COMPANY LTD." },
        new Supplier { Id = 31, Name = "700219 KAUMEYER PAPER LTD." },
        new Supplier { Id = 32, Name = "700220 KAUPP ELECTRIC LTD" },
        new Supplier { Id = 33, Name = "700222 BDI CANADA INC" },
        new Supplier { Id = 34, Name = "700226 MARMON/KEYSTONE" },
        new Supplier { Id = 35, Name = "700227 LYNN CO LTD" },
        new Supplier { Id = 36, Name = "700230 HOWARD MARTEN CO. LTD" },
        new Supplier { Id = 37, Name = "700231 MASDOM CORPORATION LTD." },
        new Supplier { Id = 38, Name = "700232 MCGEE MARKING DEVICES" },
        new Supplier { Id = 39, Name = "700233 METSO MINERALS CANADA LTD." },
        new Supplier { Id = 40, Name = "700239 NIAGARA FASTENERS" },
        new Supplier { Id = 41, Name = "700240 NIAGARA INDUSTRIAL SUPPLIES" },
        new Supplier { Id = 42, Name = "700242 NIAGARA RUBBER SUPPLY LTD" },
        new Supplier { Id = 43, Name = "700246 NORMAC ADHESIVE PRODUCTS" },
        new Supplier { Id = 44, Name = "700251 ONTARIO LASER CUTTING" },
        new Supplier { Id = 45, Name = "700259 RPM MECHANICAL INC.," },
        new Supplier { Id = 46, Name = "700261 RUSSEL METALS INC" },
        new Supplier { Id = 47, Name = "700264 SHEEHAN MECHANICAL 2002 LTD." },
        new Supplier { Id = 48, Name = "700267 SKF CANADA LIMITED" },
        new Supplier { Id = 49, Name = "700270 SPAENAUR" },
        new Supplier { Id = 50, Name = "700276 T B WOODS CANADA LTD" },
        new Supplier { Id = 51, Name = "700279 SUPERIOR PETROFUELS" },
        new Supplier { Id = 52, Name = "700280 THORDON BEARINGS INC" },
        new Supplier { Id = 53, Name = "700284 KUBES ALLOY INC." },
        new Supplier { Id = 54, Name = "700285 T W DODD MACHINE WORKS LTD" },
        new Supplier { Id = 55, Name = "700288 GUILLEVIN INTERNATIONAL INC" },
        new Supplier { Id = 56, Name = "700291 IAN JONES SALES CO." },
        new Supplier { Id = 57, Name = "700300 CMC FORGINGS INC." },
        new Supplier { Id = 58, Name = "700305 CANADA RUBBER GROUP INC." },
        new Supplier { Id = 59, Name = "700306 ECONOPRINT INC" },
        new Supplier { Id = 60, Name = "700307 BIRMINGHAM FIRE EXTINGUISHERS" },
        new Supplier { Id = 61, Name = "700308 CANADIAN BEARINGS LTD" },
        new Supplier { Id = 62, Name = "700311 SEAWAY FLUID POWER GROUP LTD" },
        new Supplier { Id = 63, Name = "700318 KALA HOME HARDWARE" },
        new Supplier { Id = 64, Name = "700319 ACKLANDS-GRAINGER INC DO NOT USE" },
        new Supplier { Id = 65, Name = "700323 GIROTTI MACHINE WORKS" },
        new Supplier { Id = 66, Name = "700325 DON'S SPRING REPAIR & SERVICE" },
        new Supplier { Id = 67, Name = "700327 SALIT STEEL NIAGARA LTD." }
                        );
                    context.SaveChanges();
                }
                if (!context.Ncrs.Any())
                {
                    context.Ncrs.AddRange(
                        new NCR
                        {
                            NCRNumber = "2024-001",
                            PONumber = 25338,
                            SupplierOrRecInspID = 1,
                            UseAsIsId = 1,
                       
                            SupplierId = 1,
                            SapId= "207956254",
                            QuantityReceived = 30,
                            QuantityDefected = 2,
                            DescriptionItemID = "1/4 .120",
                            DescriptionDefect = "Over drove the shoot wire causing the cloth to have pronounced wave in it",
                            ItemMarkedID = 1,
                           
                            reviewyes=true,
                            reviewno=false,
                            CustomerNO=true,
                            CustomerYes=false,
                           
                            RepresentativesName = "R.May",
                            Date = new DateTime(2024, 01, 02),
                            Status = "Engineering"

                        },
                        new NCR
                        {
                            NCRNumber = "2024-002",
                            PONumber = 25678,
                            SupplierOrRecInspID = 1,
                            UseAsIsId = 1,
                         
                            reviewyes = true,
                            reviewno = false,
                            CustomerNO = true,
                            CustomerYes = false,
                            SupplierId = 2,
                            SapId = "207956322",
                            QuantityReceived = 40,
                            QuantityDefected = 1,
                            DescriptionItemID = "207956322 - 3/16 .092",
                            DescriptionDefect = "Loose wires",
                            ItemMarkedID=1,
                            RepresentativesName = "R.May",
                            Date = new DateTime(2024, 01, 03),
                            Status = "Engineering"
                        },
                        new NCR
                        {
                            NCRNumber = "2024-003",
                            PONumber = 4500745548,
                            SupplierOrRecInspID = 2,
                            SupplierId = 3,
                            SapId = "203736195",
                            QuantityReceived = 16,
                            QuantityDefected = 16,
                            DescriptionItemID = "backing shield, 0.75 0.875 2.25 T316 ~",
                            DescriptionDefect = "Incorrect Dimensions.. See photos. Packing slip and package labels do not match the actual\r\ndimensions of the washers",
                            ItemMarkedID = 2,

                            RepresentativesName = "R.May",
                            Date = new DateTime(2024, 10, 26),
                            Engineer = "dtakev",
                            EngineerDate = new DateTime(2024, 1, 22),
                            UseAsIsId = 4,
                          
                            reviewyes = true,
                            reviewno = false,
                            CustomerNO = true,
                            CustomerYes = false,

                            Disposition = "N/A",
                            DrawingYes = false,
                            DrawingNo = true,


                            Status = "Purchase"
                        },
                         new NCR
                         {
                             NCRNumber = "2024-004",
                             PONumber = 4500747517,
                             SupplierOrRecInspID = 1,

                             SupplierId = 4,
                             SapId = "200013534",
                             QuantityReceived = 16,
                             QuantityDefected = 16,
                             DescriptionItemID = "Bearing Housing",
                             DescriptionDefect = "Bottom Bore has poor quality finish",
                             ItemMarkedID = 2,
                           
                     

                             RepresentativesName = "Jamie",
                             Date = new DateTime(2024, 01, 04),

                             UseAsIsId = 3,

                             reviewyes = true,
                             reviewno = false,
                             CustomerNO = true,
                             CustomerYes = false,

                             Disposition = "N/A",
                             DrawingYes = false,
                             DrawingNo = true,
                             Engineer = "dtakev",
                             EngineerDate = new DateTime(2024, 1, 22),
                             Status = "Purchase"
                         },
                         new NCR
                         {
                             NCRNumber = "2024-005",
                             PONumber = 4500745301,
                             SupplierOrRecInspID = 1,

                             SupplierId = 3,
                             SapId = "202093244",
                             QuantityReceived = 12,
                             QuantityDefected = 1,
                             DescriptionItemID = "18m .017 with seals ",
                             DescriptionDefect = "Screw in the bottom of the crate went through the bottom piece causing 2 holes.",
                             ItemMarkedID = 1,


                             RepresentativesName = "jamie",
                             Date = new DateTime(2024, 01, 03),
                             UseAsIsId = 2,
                             reviewyes = true,
                             reviewno = false,
                             CustomerNO = true,
                             CustomerYes = false,

                             Disposition = "N/A",
                             DrawingYes = false,
                             DrawingNo = true,
                             Engineer = "dtakev",
                             EngineerDate = new DateTime(2024, 1, 22),


                             PreliminaryDecision = "Use as is per Darcy Bishop (see email on page 3), longer slot length will not create a problem for the\r\ncustomer.",
                             CARYes = false,
                             CARNo = true,
                             FollowupYes = false,
                             FollowupNo = true,
                             OperatingManagerName = "L.Pentland",
                             PurchaseDate = new DateTime(2024, 02, 08),
                             Status = "Closed",

                             reinyes = true,
                             reino = false,
                             ncrclosenyes = true,
                             ncrcloseno = false,

                             InspectorName = "jamie ",
                             reviewDate = new DateTime(2024, 01, 20),
                             NcrClosed = "yes",
                             Qualitydepartment = "jamie",
                             finalDate = new DateTime(2024, 01, 20)
                         },
                          new NCR
                          {
                              NCRNumber = "2024-006",
                              PONumber = 4500720018,
                              SupplierOrRecInspID = 2,

                              SupplierId = 4,
                              SapId = "200008110",
                              QuantityReceived = 16,
                              QuantityDefected = 16,
                              DescriptionItemID = "Side Arm",
                              DescriptionDefect = "Painted wrong color",
                              ItemMarkedID = 1,

                              RepresentativesName = "Jamie",
                              Date = new DateTime(2024, 12, 14),
                              UseAsIsId = 4,

                              reviewyes = true,
                              reviewno = false,
                              CustomerNO = true,
                              CustomerYes = false,
                              
                              Disposition = "N/A",
                              DrawingYes = false,
                              DrawingNo = true,
                              Engineer = "dtakev",
                              EngineerDate = new DateTime(2024, 1, 22),
                         

                              PreliminaryDecision = "Local fabricator to repaint Haver Grey and we will bill Western Cast for the costs.",
                              CARYes = false,
                              CARNo = true,
                              FollowupYes = false,
                              FollowupNo = true,
                              OperatingManagerName = "L.Pentland",
                              PurchaseDate = new DateTime(2023, 12, 18),
                              Status = "Review",


                          },
                          new NCR
                          {
                              NCRNumber = "2024-007",
                              PONumber = 4500755515,
                              SupplierOrRecInspID = 2,
                             
                              SupplierId = 5,
                              SapId = "202093244",
                              QuantityReceived = 2,
                              QuantityDefected = 1,
                              DescriptionItemID = " 18m .017 with seals ",
                              DescriptionDefect = "Screw in the bottom of the crate went through the bottom piece causing 2 holes.",
                              ItemMarkedID = 1,

                              RepresentativesName = "R.May",
                              Date = new DateTime(2024, 12, 22),
                              UseAsIsId = 3,

                              Disposition = "N/A",
                              DrawingYes = false,
                              DrawingNo = true,
                              Engineer = "dtakev",
                              EngineerDate = new DateTime(2024, 1, 22),
                              reviewyes = true,
                              reviewno = false,
                              CustomerNO = true,
                              CustomerYes = false,

                              PreliminaryDecision = "Rework per engineering disposition",
                              CARYes = false,
                              CARNo = true,
                              FollowupYes = false,
                              FollowupNo = true,
                              OperatingManagerName = "L.Pentland",
                              PurchaseDate = new DateTime(2023, 12, 18),
                              Status = "Review",

                          },
                          new NCR
                          {
                              NCRNumber = "2024-008",
                              PONumber = 4500753128,
                              SupplierOrRecInspID = 1,

                              SupplierId = 5,
                              SapId = "208670449",
                              QuantityReceived = 4,
                              QuantityDefected = 4,
                              DescriptionItemID = "Mounting plate",
                              DescriptionDefect = "Plate was made with wrong material. Was made of Stainless steel. Should have been 10 GA ST",
                              ItemMarkedID = 2,

                              RepresentativesName = "Ray",
                              Date = new DateTime(2024, 08, 18),
                              UseAsIsId = 2,

                              reviewyes = true,
                              reviewno = false,
                              CustomerNO = true,
                              CustomerYes = false,
                              

                              reinyes = true,
                              reino = false,
                              ncrclosenyes = true,
                              ncrcloseno = false,

                              Disposition = "N/A",
                              DrawingYes = false,
                              DrawingNo = true,
                              Engineer = "dtakev",
                              EngineerDate = new DateTime(2024, 1, 22),
                        

                              PreliminaryDecision = "Replacement required.",
                              CARYes = false,
                              CARNo = true,
                              FollowupYes = false,
                              FollowupNo = true,
                              OperatingManagerName = "L.Pentland",
                              PurchaseDate = new DateTime(2024, 01, 19),
                              Status = "Review",
                            
                              InspectorName = "jamie ",
                              reviewDate = new DateTime(2024, 01, 20),
                              NcrClosed = "yes",
                              Qualitydepartment = "jamie",
                              finalDate = new DateTime(2024, 01, 20),

                      
                          },
                          new NCR
                          {
                              NCRNumber = "2024-009",
                              PONumber = 4500753159,
                              SupplierOrRecInspID = 1,

                              SupplierId = 3,
                              SapId = "2033223455",
                              QuantityReceived = 18,
                              QuantityDefected = 4,
                              DescriptionItemID = "Side Arm",
                              DescriptionDefect = "Problem with the vaul",
                              ItemMarkedID = 2,

                              RepresentativesName = "P.Ray",
                              Date = new DateTime(2024, 01, 20),
                              UseAsIsId = 1,
                              reviewyes = true,
                              reviewno = false,
                              CustomerNO = true,
                              CustomerYes = false,

                              Disposition = "N/A",
                              DrawingYes = false,
                              DrawingNo = true,
                              Engineer = "dtakev",
                              EngineerDate = new DateTime(2024, 1, 22),
                            
                              PreliminaryDecision = "Replacement required.",
                              CARYes = false,
                              CARNo = true,
                              FollowupYes = false,
                              FollowupNo = true,
                              OperatingManagerName = "L.Pentland",
                              PurchaseDate = new DateTime(2024, 01, 19),
                              Status = "Engineering",
                        

                              InspectorName = "jamie ",
                              reviewDate = new DateTime(2024, 01, 20),
                              NcrClosed = "yes",
                              Qualitydepartment = "jamie",
                              finalDate = new DateTime(2024, 01, 20),

                              reinyes = true,
                              reino = false,
                              ncrclosenyes = true,
                              ncrcloseno = false
                          },
                          new NCR
                          {
                              NCRNumber = "2024-010",
                              PONumber = 4500757930,
                              SupplierOrRecInspID = 1,

                              SupplierId = 2,
                              SapId = "201087268",
                              QuantityReceived = 6,
                              QuantityDefected = 6,
                              DescriptionItemID = " 1x1 Panel 6.3mm x 50mm",
                              DescriptionDefect = "Slot lengths are 53.9mm to 54.04 should be 50mm",
                              ItemMarkedID = 1,


                              RepresentativesName = "R.May",
                              Date = new DateTime(2024, 08, 01),
                              UseAsIsId = 3,

                              reviewyes = true,
                              reviewno = false,
                              CustomerNO = true,
                              CustomerYes = false,

                              Disposition = "N/A",
                              DrawingYes = false,
                              DrawingNo = true,
                              Engineer = "dtakev",
                              EngineerDate = new DateTime(2024, 1, 22),


                              PreliminaryDecision = "Replacement required.",
                              CARYes = false,
                              CARNo = true,
                              FollowupYes = false,
                              FollowupNo = true,
                              OperatingManagerName = "L.Pentland",
                              PurchaseDate = new DateTime(2024, 01, 18),
                              Status = "Closed",

                              InspectorName = "jamie ",
                              reviewDate = new DateTime(2024, 01, 20),
                              NcrClosed = "yes",
                              Qualitydepartment = "jamie",
                              finalDate = new DateTime(2024, 01, 20)
                          },
                             new NCR
                             {
                                 NCRNumber = "2024-011",
                                 PONumber = 4500753988,
                                 SupplierOrRecInspID = 2,

                                 SupplierId = 4,
                                 SapId = "2086749899",
                                 QuantityReceived = 10,
                                 QuantityDefected = 33,
                                 DescriptionItemID = "Defect in Steel",
                                 DescriptionDefect = "Problem with the vaul",
                                 ItemMarkedID = 2,

                                 RepresentativesName = "P.Ray",
                                 Date = new DateTime(2024, 01, 19),
                                 UseAsIsId = 2,
                                 reviewyes = true,
                                 reviewno = false,
                                 CustomerNO = true,
                                 CustomerYes = false,

                                 Disposition = "N/A",
                                 DrawingYes = false,
                                 DrawingNo = true,
                                 Engineer = "dtakev",
                                 EngineerDate = new DateTime(2024, 1, 22),

                                 PreliminaryDecision = "Replacement required.",
                                 CARYes = false,
                                 CARNo = true,
                                 FollowupYes = false,
                                 FollowupNo = true,
                                 OperatingManagerName = "L.Pentland",
                                 PurchaseDate = new DateTime(2024, 01, 19),
                                 Status = "Engineering",


                                 InspectorName = "jamie ",
                                 reviewDate = new DateTime(2024, 01, 20),
                                 NcrClosed = "yes",
                                 Qualitydepartment = "jamie",
                                 finalDate = new DateTime(2024, 01, 20),

                                 reinyes = true,
                                 reino = false,
                                 ncrclosenyes = true,
                                 ncrcloseno = false
                             }, new NCR
                             {
                                 NCRNumber = "2024-012",
                                 PONumber = 5667789644,
                                 SupplierOrRecInspID = 2,

                                 SupplierId = 5,
                                 SapId = "202093244",
                                 QuantityReceived = 2,
                                 QuantityDefected = 1,
                                 DescriptionItemID = " 18m .017 with seals ",
                                 DescriptionDefect = "Screw in the bottom of the crate went through the bottom piece causing 2 holes.",
                                 ItemMarkedID = 1,

                                 RepresentativesName = "R.May",
                                 Date = new DateTime(2024, 12, 22),
                                 UseAsIsId = 3,

                                 Disposition = "N/A",
                                 DrawingYes = false,
                                 DrawingNo = true,
                                 Engineer = "dtakev",
                                 EngineerDate = new DateTime(2024, 1, 22),
                                 reviewyes = true,
                                 reviewno = false,
                                 CustomerNO = true,
                                 CustomerYes = false,

                                 PreliminaryDecision = "Rework per engineering disposition",
                                 CARYes = false,
                                 CARNo = true,
                                 FollowupYes = false,
                                 FollowupNo = true,
                                 OperatingManagerName = "L.Pentland",
                                 PurchaseDate = new DateTime(2023, 12, 18),
                                 Status = "Engineering",

                             }, new NCR
                             {
                                 NCRNumber = "2024-013",
                                 PONumber = 7848445959,
                                 SupplierOrRecInspID = 2,

                                 SupplierId = 5,
                                 SapId = "202093244",
                                 QuantityReceived = 2,
                                 QuantityDefected = 1,
                                 DescriptionItemID = " 18m .017 with seals ",
                                 DescriptionDefect = "Screw in the bottom of the crate went through the bottom piece causing 2 holes.",
                                 ItemMarkedID = 1,

                                 RepresentativesName = "R.May",
                                 Date = new DateTime(2024, 12, 22),
                                 UseAsIsId = 3,

                                 Disposition = "N/A",
                                 DrawingYes = false,
                                 DrawingNo = true,
                                 Engineer = "dtakev",
                                 EngineerDate = new DateTime(2024, 1, 22),
                                 reviewyes = true,
                                 reviewno = false,
                                 CustomerNO = true,
                                 CustomerYes = false,

                                 PreliminaryDecision = "Rework per engineering disposition",
                                 CARYes = false,
                                 CARNo = true,
                                 FollowupYes = false,
                                 FollowupNo = true,
                                 OperatingManagerName = "L.Pentland",
                                 PurchaseDate = new DateTime(2023, 12, 18),
                                 Status = "Purchase",

                             }, new NCR
                             {
                                 NCRNumber = "2024-014",
                                 PONumber = 3445667788,
                                 SupplierOrRecInspID = 2,

                                 SupplierId = 5,
                                 SapId = "202093244",
                                 QuantityReceived = 2,
                                 QuantityDefected = 1,
                                 DescriptionItemID = " 18m .017 with seals ",
                                 DescriptionDefect = "Screw in the bottom of the crate went through the bottom piece causing 2 holes.",
                                 ItemMarkedID = 1,

                                 RepresentativesName = "R.May",
                                 Date = new DateTime(2024, 12, 22),
                                 UseAsIsId = 3,

                                 Disposition = "N/A",
                                 DrawingYes = false,
                                 DrawingNo = true,
                                 Engineer = "dtakev",
                                 EngineerDate = new DateTime(2024, 1, 22),
                                 reviewyes = true,
                                 reviewno = false,
                                 CustomerNO = true,
                                 CustomerYes = false,

                                 PreliminaryDecision = "Rework per engineering disposition",
                                 CARYes = false,
                                 CARNo = true,
                                 FollowupYes = false,
                                 FollowupNo = true,
                                 OperatingManagerName = "L.Pentland",
                                 PurchaseDate = new DateTime(2023, 12, 18),
                                 Status = "Purchase",

                             }, new NCR
                             {
                                 NCRNumber = "2024-015",
                                 PONumber = 3445667788,
                                 SupplierOrRecInspID = 2,

                                 SupplierId = 5,
                                 SapId = "202093244",
                                 QuantityReceived = 2,
                                 QuantityDefected = 1,
                                 DescriptionItemID = " 18m .017 with seals ",
                                 DescriptionDefect = "Screw in the bottom of the crate went through the bottom piece causing 2 holes.",
                                 ItemMarkedID = 1,

                                 RepresentativesName = "R.May",
                                 Date = new DateTime(2024, 12, 22),
                                 UseAsIsId = 3,

                                 Disposition = "N/A",
                                 DrawingYes = false,
                                 DrawingNo = true,
                                 Engineer = "dtakev",
                                 EngineerDate = new DateTime(2024, 1, 22),
                                 reviewyes = true,
                                 reviewno = false,
                                 CustomerNO = true,
                                 CustomerYes = false,

                                 PreliminaryDecision = "Rework per engineering disposition",
                                 CARYes = false,
                                 CARNo = true,
                                 FollowupYes = false,
                                 FollowupNo = true,
                                 OperatingManagerName = "L.Pentland",
                                 PurchaseDate = new DateTime(2023, 12, 18),
                                 Status = "Purchase",

                             }
                          
                          );

                    context.SaveChanges();
                }

                if (!context.emailAddresses.Any())
                        {
                            context.emailAddresses.AddRange(
                                  new EmailAddress
                                  {
                                      Name = "Quality",
                                      Address = "gsingh2220@ncstudents.niagaracollege.ca"
                                  },
                                new EmailAddress
                                {
                                    Name = "Engineering",
                                    Address = "gsingh2220@ncstudents.niagaracollege.ca"
                                },
                                 new EmailAddress
                                 {
                                     Name = "Purchase",
                                     Address = "gsingh2220@ncstudents.niagaracollege.ca"
                                 },
                                  new EmailAddress
                                  {
                                      Name = "Review",
                                      Address = "gsingh2220@ncstudents.niagaracollege.ca"
                                  }
                                );
                            context.SaveChanges();
                        }
                    
            }
        
            catch (DbUpdateException ex)
            {
                // Log or handle the exception
                Console.WriteLine("DbUpdateException occurred: " + ex.Message);

                // If there's an inner exception, log or handle it as well
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                // You can also rethrow the exception if needed

            }
        }
    }
}
