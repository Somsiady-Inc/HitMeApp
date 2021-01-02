SELECT 'CREATE DATABASE mydb' WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'hitmeapp')\gexec
GRANT ALL PRIVILEGES ON DATABASE hitmeapp TO postgres;

\connect hitmeapp

-- Identity Module
CREATE SCHEMA IF NOT EXISTS identity;

CREATE TABLE IF NOT EXISTS identity."user"
(
    id uuid NOT NULL PRIMARY KEY,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    created_at timestamp without time zone DEFAULT (now() at time zone 'utc'),
    updated_at timestamp without time zone DEFAULT (now() at time zone 'utc')
);

ALTER TABLE identity."user" OWNER to postgres;