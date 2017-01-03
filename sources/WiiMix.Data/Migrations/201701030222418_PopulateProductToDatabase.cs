namespace WiiMix.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateProductToDatabase : DbMigration
    {
        public override void Up()
        {
            // Sample data

            Sql("INSERT INTO Categories(Name) VALUES('Card Reader')");
            Sql("INSERT INTO Categories(Name) VALUES('Computers')");
            Sql("INSERT INTO Categories(Name) VALUES('Headphones')");
            Sql("INSERT INTO Categories(Name) VALUES('IPad')");
            Sql("INSERT INTO Categories(Name) VALUES('Memory Cards')");
            Sql("INSERT INTO Categories(Name) VALUES('Mobiles')");
            Sql("INSERT INTO Categories(Name) VALUES('Operating System')");
            Sql("INSERT INTO Categories(Name) VALUES('Pen Drives')");
            Sql("INSERT INTO Categories(Name) VALUES('Tablets')");

            Sql("INSERT INTO Brands(Name) VALUES('Apple')");
            Sql("INSERT INTO Brands(Name) VALUES('Dell')");
            Sql("INSERT INTO Brands(Name) VALUES('HP')");
            Sql("INSERT INTO Brands(Name) VALUES('LG')");
            Sql("INSERT INTO Brands(Name) VALUES('Microsoft Corporations')");
            Sql("INSERT INTO Brands(Name) VALUES('Moserbear')");
            Sql("INSERT INTO Brands(Name) VALUES('Panasonic')");
            Sql("INSERT INTO Brands(Name) VALUES('Philips')");
            Sql("INSERT INTO Brands(Name) VALUES('Samsung Electronics ltd.')");
            Sql("INSERT INTO Brands(Name) VALUES('Sandisk')");
            Sql("INSERT INTO Brands(Name) VALUES('Sony Inc.')");

            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Apple iPad mini White & Silver 16 GB',4,1)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Dell Inspiron 15 3521',2,2)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Dell Inspiron 15R 5521',2,2)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Dell inspiron 2012',2,2)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Google Nexus 4 Black',6,4)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('HP 1000-1401AU Laptop',2,3)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Microsoft Windows 8 Pro 64-bit',7,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia 1100',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 1020',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 510',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 610',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 620',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 625',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 720',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 820',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 920',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Nokia Lumia 925',6,5)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Samsung Galaxy S4 Mini GT-I9192',6,9)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Samsung Galaxy Tab',9,9)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Sony Xperia C Black',6,11)");
            Sql("INSERT INTO Products(Name, CategoryId, BrandId) VALUES('Sony Xperia L',6,11)");


            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(1,'7.9inch LED Backlit Display Over 275000 Apps on the App Store A5 Chip FaceTime HD Camera Upto 10 Hrs of Battery Life iOS 6 and iCloud 5MP iSight Camera with 1080p HD Video Recording 1.2 MP Secondary Camera Wi - Fi Audio Player Video Player Bluetooth SUPC: SDL380152620',28080,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(2,'Key Features : Processor: Intel Core i3 - 3217U RAM: 4 GB DDR3 Hard Drive: 500 GB Operating System: Windows 8(64 - Bit)',34230,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(3,'Brand	Dell Series  Inspiron Colour  Black Screen Size 15.6 Inches Item model number   15R 5521 Processor Brand Intel Processor Type  Core i3 - 3217U RAM Size    4 GB Hard Drive Size 500 GB Graphics Coprocessor    Integrated Hardware Platform   PC Operating System    Windows 8',39490,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(5,'Android v4.2 (Jelly Bean) OS 4.7 - inch WXGA True HD IPS Plus Display 8 MP Primary Camera with LED Flash and 1.3 MP Secondary Camera Photo Sphere Camera 2100 mAh Sio + Battery Snapdragon S4 Pro 1.5 GHz Quad Core Processor(Adreno 320 GPU) 16 GB Internal Memory Wireless Charging(Use any Qi - compatible Charger) GPRS / EDGE 3G Bluetooth Music Player Video Player 3.5mm Audio Jack',22449,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(6,'Colour: Glossy Imprint Black Licorice Colour Operating System: Windows 8 Hard Disk Capacity: 500GB Weight: 2.2 kg Screen Size: 14.0 - inch RAM: 2GB Processor Name: AMD Dual - core APU Processor E1 - 1500 Keyboard: Full - size Textured Pocket Keyboard Altec Lansing Stereo Speakers SUPC: SDL709658791',22265,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(7,'Windows has also made changes to make your PC more secure by boosting its existing security features and adding SmartScreen, which acts to prevent suspicious programs or apps from being installed or running on your machine. Finally, Windows 8 also gives you the ability to refresh itself to give users a new starting point and a cleaner version of Windows.Vibrant and beautiful, the Start screen is the first thing you will see. Each tile on the Start screen is connected to a person, app, website, playlist, or whatever else is important to you. Tiles light up with the latest info, so you are instantly up to date. In one glance, you will see that photo you were just tagged in, tomorrows weather, and messages from your friends.',9490,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(9,'Main camera sensor: 41 MP, PureView Flash type: Xenon flash Display size: 11.43cm Touch screen technology: Super sensitive touch Processor name: Qualcomm Snapdragon™ S4 Maximum talk time(3G): 13.3h Wireless charging: Yes, with accessory cover',49999,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(10,'Main camera sensor: 5 MP Display size: 10.16cm Maximum talk time(3G): 8.4h Maximum standby time(3G): 653.2h Maximum music playback time: 36h Photo sharing: Facebook; Send as email attachment; SkyDrive',8000,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(11,'Main camera sensor: 5 MP Display size: 9.4cm Maximum talk time(3G): 10h Maximum standby time(3G): 785h Maximum music playback time: 35h Dual SIM: No',8999,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(12,'Main camera sensor: 5 MP Display size: 3.8 Processor name: Qualcomm Snapdragon™ S4 Maximum talk time(3G): 9.9h Maximum standby time(3G): 330h Maximum music playback time: 61h Photo sharing: Share over Bluetooth; Facebook; Flickr; Send as email attachment; Share on TV with Play to DLNA app; SkyDrive; PhotoBeamer',12300,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(13,'Main camera sensor: 5 MP Display size: 11.94cm Processor name: Qualcomm Snapdragon™ S4 Maximum video playback time: 6.8h Maximum music playback time: 90h Touch screen technology: Super sensitive touch',17000,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(14,'Main camera sensor: 6.7 MP Display size: 10.92cm Processor name: Qualcomm Snapdragon™ S4 Maximum talk time(3G): 13.4h Maximum standby time(3G): 520h Maximum music playback time: 79h Photo sharing: Share over Bluetooth; Facebook; Tap and share images or videos with NFC; Picasa; Flickr; Send as email attachment; Share on TV with Play to DLNA app; SkyDrive; PhotoBeamer',17100,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(15,'Main camera sensor: 8.7 MP Display size: 10.92cm Processor name: Qualcomm Snapdragon™ S4 Maximum talk time(3G): 8.1h Maximum standby time(3G): 360h Maximum music playback time: 61h Photo sharing: Share over Bluetooth; Facebook; Tap and share images or videos with NFC; Picasa; Flickr; Send as email attachment; Share on TV with Play to DLNA app; SkyDrive; PhotoBeamer',24500,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(17,'Main camera sensor: 8.7 MP PureView Display size: 11.43cm Processor name: Qualcomm Snapdragon™ S4 Maximum standby time(3G): 440h Maximum video playback time: 6.6h Maximum music playback time: 55h Touch screen technology: Super sensitive touch',28500,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(18,'1. 8MP primary camera with LED flash, 1920 x 1080 HD video recording, auto focus, geo-tagging, touch focus, face detection, HDR, panorama, CMOS, image editor and 1.9MP front facing camera 2.  4.27 - inch Super AMOLED capacitive touchscreen with 540 x 960 pixels resolution and 16M color support 3.Android Jelly Bean 4.2.2 operating system with 1.7GHz dual core processor, 8GB internal memory expandable up to 64GB and dual sim1900mAH battery providing talk-time of 12 hours and standby time of 300 hours 4. 1 year manufacturer warranty for device and 6 months manufacturer warranty for in-box accessories including batteries from the date of purchase',22700,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(20,'1.2 GHz MTK6589 Quad Core Processor 8 MP Rear & 0.3 MP Front Camera 3G FM Radio Video Player GPRS 3.5mm Audio Jack 5 Inch Capacitive Touch Screen Dual SIM Expandable Memory Up to 32GB Android 4.2.2 Jelly Bean OS Music Player Bluetooth Wi - Fi',19007,'')");
            Sql("INSERT INTO Configs(ProductId, Feature, Price, Image) VALUES(21,'1GB RAM Wi - Fi 3.5mm Audio Jack FM Radio VGA Front Camera Android 4.1 Jelly Bean Music Player 1 GHz Qualcomm MSM8230 dual - core Processor Video Player GPRS Bluetooth 3G 4.3 Inch Capacitive touchscreen with on - screen QWERTY keyboard 8MP Primary Camera 8GB Internal Memory Expandable Up to 32GB',15838,'')");


        }
        
        public override void Down()
        {
            Sql("DELETE FROM Categories WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9)");
            Sql("DELETE FROM Brands WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)");
            Sql("DELETE FROM Products WHERE Id IN(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21)");
            Sql("DELETE FROM Configs WHERE ProductId IN(1,2,3,5,6,7,9,10,11,12,13,14,15,17,18,20,21)");
        }
    }
}
