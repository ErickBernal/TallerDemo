# Frontend implementado en Vue

Fuente de consulta
-> [https://vuejs.org](https://vuejs.org/guide/quick-start.html)

Comandos

## Vue

```bash
# USANDO VUE

# instar libreria
    npm install -g @vue/cli
    npm install vuex

# comprobar instalacion
    vue --version

# Crear proyecto
    vue create taller
    cd taller
    npm run serve

```

## Vite

```bash
# USANDO VITE

# Creamos el proyecto
    npm create vite
#seleccionar el nombre del proyecto
    viteTaller
# Seleccionar el framework a utilizar
    Vue
#Entrar al proyecto
    cd viteTaller
# descargar dependencias
    npm i
#Ejecutar
    npm run dev
# transformar el codigo para produccion
    npm run build
```

## Dependencias

```bash
#Instalar
    npm install vue-router
```

# Implementando Quasar

para relizar una actualizacion entre
Older V2 to latest v2

- > [Documentacion oficial](https://quasar.dev/start/upgrade-guide#with-quasar-cli)

- > [Fuente de consulta](https://quasar.dev/start/quick-start)

```bash
# Instalar dependencias
    npm install --global yarn
    npm i -g @quasar/cli
    yarn add axios
    yarn add --dev dotenv

# Crear proyecto
    yarn create quasar
    # Nota: seleccionar Axios
# ejecutar
    cd <nombre_carpeta>
    quasar dev
```

# Para relizar la comunicacion hacia el backend

- Es neceario crear un archivo **.env** en la [rais](./QuasarTaller/)
- En el archivo **.env** crear la variable

```bash
    API_BASE_URL=https://localhost:7099
```

- Luego el archivo [quasar.config.js](./QuasarTaller/quasar.config.js), se encargara de realizar la importacion.
- Agregado a **_quasar.config.js_**

```js
    require("dotenv").config();
    env: {
        API_BASE_URL: process.env.API_BASE_URL,
    },
```
