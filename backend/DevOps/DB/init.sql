SELECT 'CREATE DATABASE hitmeapp' WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'hitmeapp')\gexec
GRANT ALL PRIVILEGES ON DATABASE hitmeapp TO postgres;

\connect hitmeapp

-- Identity Module
CREATE SCHEMA IF NOT EXISTS "identity";

CREATE TABLE IF NOT EXISTS "identity"."user"
(
    id uuid NOT NULL PRIMARY KEY,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    created_at timestamp without time zone DEFAULT (now() at time zone 'utc'),
    updated_at timestamp without time zone DEFAULT (now() at time zone 'utc')
);

ALTER TABLE "identity"."user" OWNER to postgres;

-- User's Module
CREATE SCHEMA IF NOT EXISTS "user";

CREATE TABLE IF NOT EXISTS "user"."user"
(
    id uuid NOT NULL PRIMARY KEY,
    nickname VARCHAR(20) NULL,
    description VARCHAR(255) NULL,
    birth_date timestamp without time zone DEFAULT (now() at time zone 'utc') NULL,
    sex SMALLINT NULL,
    latitude float(8) NULL,
    longitude float(8) NULL
);

ALTER TABLE "user"."user" OWNER to postgres;

CREATE TABLE IF NOT EXISTS "user"."trait"
(
    id uuid NOT NULL PRIMARY KEY,
    value VARCHAR(30) UNIQUE NOT NULL
);

ALTER TABLE "user"."trait" OWNER to postgres;

CREATE TABLE IF NOT EXISTS "user"."user_traits"
(
    user_id uuid NOT NULL,
    trait_id uuid NOT NULL,
    PRIMARY KEY (user_id, trait_id)
);

ALTER TABLE "user"."user_traits" OWNER to postgres;

CREATE TABLE IF NOT EXISTS "user"."user_preferences"
(
    user_id uuid NOT NULL,
    trait_id uuid NOT NULL,
    PRIMARY KEY (user_id, trait_id)
);

ALTER TABLE "user"."user_preferences" OWNER to postgres;
