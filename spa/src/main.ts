import Vue from "vue";
import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import App from "./App.vue";
import router from "./router";
import moment from "moment";
import store from "./store";

Vue.use(BootstrapVue);

Vue.filter("formatDate", function (value: string) {
    if (value) {
        return moment(String(value)).format("MMMM DD - YYYY");
    }
});

Vue.config.productionTip = false;

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount("#app");
