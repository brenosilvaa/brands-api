# BASE - Criação da estrutura de pastas base
FROM nginx:alpine AS base
RUN mkdir /app
WORKDIR /app
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80

# PUBLISH - Publicação da aplicação
FROM node:lts-alpine AS publish
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# FINAL - Cópia dos arquivos do publish para a estrutura de pastas base
FROM base AS final
WORKDIR /dist
COPY --from=publish /app/dist/brand-fe-web-admin /usr/share/nginx/html

# Rodar Nginx
CMD ["/bin/sh",  "-c",  "exec nginx -g 'daemon off;'"]