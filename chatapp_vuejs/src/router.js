import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

import ChatClient from "./components/ChatClient";

export const routes = [{ path: "/chat", component: ChatClient }];
