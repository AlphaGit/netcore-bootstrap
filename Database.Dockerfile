FROM postgres:11

COPY certificates/public.key /var/lib/postgresql/server.key
COPY certificates/public.crt /var/lib/postgresql/server.crt
RUN chown postgres /var/lib/postgresql/server.key && \
    chmod 600 /var/lib/postgresql/server.key

CMD ["postgres", "-cssl=true", "-cssl_cert_file=/var/lib/postgresql/server.crt", "-cssl_key_file=/var/lib/postgresql/server.key"]