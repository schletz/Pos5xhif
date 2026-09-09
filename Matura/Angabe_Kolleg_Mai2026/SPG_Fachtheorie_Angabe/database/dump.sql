CREATE TABLE "category" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_category" PRIMARY KEY AUTOINCREMENT,
    "text" TEXT NOT NULL
);


CREATE TABLE "person" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_person" PRIMARY KEY AUTOINCREMENT,
    "firstName" TEXT NOT NULL,
    "lastName" TEXT NOT NULL,
    "schoolClass" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "phone" TEXT NOT NULL
);


CREATE TABLE "staff" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_staff" PRIMARY KEY AUTOINCREMENT,
    "firstName" TEXT NOT NULL,
    "lastName" TEXT NOT NULL
);


CREATE TABLE "status" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_status" PRIMARY KEY AUTOINCREMENT,
    "text" TEXT NOT NULL
);


CREATE TABLE "inventoryItem" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_inventoryItem" PRIMARY KEY AUTOINCREMENT,
    "serialNumber" TEXT NOT NULL,
    "itemName" TEXT NOT NULL,
    "categoryId" INTEGER NOT NULL,
    "isAvailable" INTEGER NOT NULL,
    CONSTRAINT "FK_inventoryItem_category_categoryId" FOREIGN KEY ("categoryId") REFERENCES "category" ("id") ON DELETE RESTRICT
);


CREATE TABLE "reservation" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_reservation" PRIMARY KEY AUTOINCREMENT,
    "personId" INTEGER NOT NULL,
    "inventoryItemId" INTEGER NOT NULL,
    "loanDate" TEXT NOT NULL,
    "statusId" INTEGER NOT NULL,
    "returnDate" TEXT NULL,
    "acceptedById" INTEGER NULL,
    CONSTRAINT "FK_reservation_inventoryItem_inventoryItemId" FOREIGN KEY ("inventoryItemId") REFERENCES "inventoryItem" ("id") ON DELETE RESTRICT,
    CONSTRAINT "FK_reservation_person_personId" FOREIGN KEY ("personId") REFERENCES "person" ("id") ON DELETE RESTRICT,
    CONSTRAINT "FK_reservation_staff_acceptedById" FOREIGN KEY ("acceptedById") REFERENCES "staff" ("id") ON DELETE RESTRICT,
    CONSTRAINT "FK_reservation_status_statusId" FOREIGN KEY ("statusId") REFERENCES "status" ("id") ON DELETE RESTRICT
);


CREATE INDEX "IX_inventoryItem_categoryId" ON "inventoryItem" ("categoryId");


CREATE INDEX "IX_reservation_acceptedById" ON "reservation" ("acceptedById");


CREATE INDEX "IX_reservation_inventoryItemId" ON "reservation" ("inventoryItemId");


CREATE INDEX "IX_reservation_personId" ON "reservation" ("personId");


CREATE INDEX "IX_reservation_statusId" ON "reservation" ("statusId");


