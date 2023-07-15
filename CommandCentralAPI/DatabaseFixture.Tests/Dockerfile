# This Dockerfile is used to instantiate the testing environment in relation to the postgres db

FROM postgres

ENV POSTGRES_USER commandcentraltest
ENV POSTGRES_PASSWORD commandcentraltestpass
ENV POSTGRES_DB commandcentraltest_db

# Copy SQL scripts to initialize the database (optional)
# COPY init.sql /docker-entrypoint-initdb.d/

# Expose the default postgres port
EXPOSE 5432

# Start the postgres server 
CMD ["postgres"]
