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

      {
        path: "/servicios",
        component: () => import("pages/serviceTaller/ServicesPage.vue"),
      },
      {
        path: "/servicios/nuevo",
        component: () => import("pages/serviceTaller/NewServices.vue"),
      },
      {
        path: "/servicios/estado",
        component: () => import("pages/serviceTaller/StatusServices.vue"),
      },
      {
        path: "/cliente/nuevo",
        component: () => import("pages/client/NewClient.vue"),
      },
      {
        path: "/trabajo/nuevo",
        component: () => import("src/pages/work/NewWork.vue"),
      },
      {
        path: "/vehiculo/nuevo",
        component: () => import("pages/NewVehicle.vue"),
      },

      {
        path: "/repuesto/buscar",
        component: () => import("pages/vehicleParts/FindVehicleParts.vue"),
      },
      {
        path: "/repuesto/nuevo",
        component: () => import("pages/vehicleParts/NewVehiclePart.vue"),
      },
      {
        path: "/repuestos",
        component: () => import("pages/vehicleParts/VehiclePartPage.vue"),
      },
      {
        path: "/cliente",
        component: () => import("pages/client/ClientPage.vue"),
      },
      {
        path: "/factura",
        component: () => import("pages/invoice/InvoicePage.vue"),
      },
      {
        path: "/vehiculo",
        component: () => import("pages/vehicle/VehiclePage.vue"),
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
