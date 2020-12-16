import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        loggedIn: false,
        queries: null,
        taskID: null,
        userID: null
    },
    mutations: {
        loggedInState(state, payload) {
            state.loggedIn = payload;
        },
        queriesState(state, payload) {
            state.queries = payload;
        },
        taskID(state, payload) {
            state.taskID = payload;
        },
        userID(state, payload) {
            state.userID = payload;
        }
    }
});
