import Vue from "vue";
import Vuex from "vuex";
import { Query } from "@/types/query";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        loggedIn: false,
        queries: Array<Query>()
    },
    mutations: {
        loggedInState(state, payload) {
            state.loggedIn = payload;
        },
        queriesState(state, payload) {
            state.queries = payload;
        }
    }
});
