CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "Posts" (
    "Id" UUID NOT NULL DEFAULT (uuid_generate_v4()) PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Content" TEXT
);