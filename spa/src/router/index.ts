import Vue from "vue";
import VueRouter, { RouteConfig, Route } from "vue-router";
import Home from "../views/Home.vue";
import Register from "../views/Register.vue";
import About from "../views/About.vue";
import Login from "../views/Login.vue";
import Profile from "../views/Profile.vue";
import Overview from "../views/Overview.vue";
import SeeMore from "../views/SeeMore.vue";
import ArbejdsOpgave from "../views/ArbejdsOpgave.vue";

Vue.use(VueRouter);

const ifAuthenticated = (to: Route, from: Route, next: any) => {
    if (localStorage.getItem("user")) {
        next();
        return;
    }
    // eslint-disable-next-line
    router.push({ name: "Login" });
};

const routes: Array<RouteConfig> = [
    {
        path: "/",
        name: "Home",
        component: Home
    },
    {
        path: "/register",
        name: "Register",
        component: Register
    },
    {
        path: "/about",
        name: "About",
        component: About
    },
    {
        path: "/login",
        name: "Login",
        component: Login
    },
    {
        path: "/profile/:user?",
        name: "Profile",
        component: Profile,
        beforeEnter: ifAuthenticated
    },
    {
        path: "/overview",
        name: "Overview",
        component: Overview
    },
    {
        path: "/seeMore",
        name: "SeeMore",
        component: SeeMore
    },
    {
        path: "/arbejdsopgave",
        name: "ArbejdsOpgave",
        component: ArbejdsOpgave
    }
];

const router = new VueRouter({
    routes
});

export default router;
