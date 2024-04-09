import ErrorNotFound from "pages/ErrorNotFound.vue";
import { LocalStorage } from "quasar";
const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      { path: "", component: () => import("pages/IndexPage.vue") },
      {
        path: "/log",
        component: () => import("pages/LogPage.vue"),
        beforeEnter: (to, from, next) => {
          if (LocalStorage.getItem("owner") === true) {
            next();
          } else {
            next("/not-authorized");
          }
        },
      },

      { path: "/servicios", component: () => import("pages/ServicesPage.vue") },
      {
        path: "/nuevoservicio",
        component: () => import("pages/NewServices.vue"),
      },
      {
        path: "/estatusservicio",
        component: () => import("pages/StatusServices.vue"),
      },
      {
        path: "/nuevocliente",
        component: () => import("pages/NewClient.vue"),
      },
      {
        path: "/agregartrabajo",
        component: () => import("pages/NewWork.vue"),
      },
      {
        path: "/nuevovehiculo",
        component: () => import("pages/NewVehicle.vue"),
      },

      {
        path: "/buscarrepuesto",
        component: () => import("pages/vehicleParts/FindVehicleParts.vue"),
      },
      {
        path: "/nuevorepuesto",
        component: () => import("pages/vehicleParts/NewVehiclePart.vue"),
      },
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/ErrorNotFound.vue"),
  },
  // Ruta para la página de error de autorización
  {
    path: "/not-authorized",
    component: () => import("pages/ErrorNotAuthorized.vue"),
  },
];

export default routes;
