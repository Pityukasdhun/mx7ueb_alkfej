# ---- build stage ----
FROM node:20-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .
RUN npm run build

# ---- runtime stage ----
FROM nginx:alpine
# copy either dist/<app>/ or dist/ directly (whichever exists)
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /app/dist/ /usr/share/nginx/html/
# if build output is dist/<app>/, nginx will serve a folder; move its contents up
RUN if [ -d /usr/share/nginx/html/* ] && [ "$(find /usr/share/nginx/html -maxdepth 1 -type d | wc -l)" -gt 1 ]; then \
      first_dir="$(find /usr/share/nginx/html -maxdepth 1 -type d -not -path /usr/share/nginx/html | head -n 1)"; \
      cp -a "$first_dir"/. /usr/share/nginx/html/ && rm -rf "$first_dir"; \
    fi

EXPOSE 80