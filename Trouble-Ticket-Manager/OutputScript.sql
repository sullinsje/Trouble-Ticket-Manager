CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Building" TEXT NULL,
    "Room" TEXT NULL
);

CREATE TABLE "Computers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Computers" PRIMARY KEY AUTOINCREMENT,
    "AssetTag" TEXT NOT NULL,
    "ServiceTag" TEXT NOT NULL,
    "Model" TEXT NOT NULL,
    "UserId" INTEGER NULL,
    CONSTRAINT "FK_Computers_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id")
);

CREATE TABLE "Tickets" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Tickets" PRIMARY KEY AUTOINCREMENT,
    "ContactId" INTEGER NOT NULL,
    "SubmittedAt" TEXT NOT NULL,
    "IsResolved" INTEGER NULL,
    "ChargerGiven" INTEGER NULL,
    CONSTRAINT "FK_Tickets_Users_ContactId" FOREIGN KEY ("ContactId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "TicketComputers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_TicketComputers" PRIMARY KEY AUTOINCREMENT,
    "IssueDescription" TEXT NOT NULL,
    "TicketId" INTEGER NOT NULL,
    "ComputerId" INTEGER NOT NULL,
    CONSTRAINT "FK_TicketComputers_Computers_ComputerId" FOREIGN KEY ("ComputerId") REFERENCES "Computers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_TicketComputers_Tickets_TicketId" FOREIGN KEY ("TicketId") REFERENCES "Tickets" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Computers_UserId" ON "Computers" ("UserId");

CREATE INDEX "IX_TicketComputers_ComputerId" ON "TicketComputers" ("ComputerId");

CREATE INDEX "IX_TicketComputers_TicketId" ON "TicketComputers" ("TicketId");

CREATE INDEX "IX_Tickets_ContactId" ON "Tickets" ("ContactId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251203022924_Mig1', '9.0.11');

COMMIT;

