FROM mcr.microsoft.com/mssql/server:2017-latest

COPY initDb.sql initDb.sql
COPY db-init.sh db-init.sh
COPY entrypoint.sh entrypoint.sh

RUN chmod +x /db-init.sh
RUN chmod +x /entrypoint.sh
RUN chmod +x /initDb.sql

CMD /bin/bash ./entrypoint.sh