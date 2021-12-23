FROM node:erbium-buster-slim as build

WORKDIR /app
COPY package*.json /app/
RUN npm install
RUN npm install -g @angular/cli@13.1.1

COPY . /app/
ARG configuration=production
RUN npm run build -- --output-path=./dist/out --configuration $configuration

FROM nginx:latest as run
COPY --from=build /app/dist/out/ /usr/share/nginx/html
COPY ./nginx-custom.conf /etc/nginx/conf.d/default.conf
