FROM nginx
COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
COPY ./nginx/nginx.conf /etc/nginx/proxy.conf
RUN openssl req -x509 -subj /CN=localhost -days 365 -set_serial 2 -newkey rsa:4096 -keyout /etc/nginx/cert.key -nodes -out /etc/nginx/cert.pem
EXPOSE 443