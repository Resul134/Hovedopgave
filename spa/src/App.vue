<template>
  <div id="app">
    <div id="nav">
      <TopHeader/>
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="/register">Register</router-link>
    </div>
    <div class="router-view">
      <router-view/>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import TopHeader from "@/components/TopHeader.vue";
@Component({
    components: {
        TopHeader
    }
})
export default class App extends Vue {
}

</script>

<style lang="scss">
@import "./assets/main.scss";

*, *:before, *:after {
  box-sizing: border-box;
}

h1,h2,h3,h4,h5,h6,p,span,div {
  color: $gray;
}

a {
  color: $primary;
}

.router-view {
  padding: 25px;
}
</style>
