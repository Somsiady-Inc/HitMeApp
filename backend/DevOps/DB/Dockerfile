FROM postgres:latest

LABEL maintainer="Somsiady Inc."

ENV POSTGRES_DB hitmeapp

COPY init.sql /docker-entrypoint-initdb.d/

EXPOSE 5432