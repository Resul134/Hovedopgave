<template>
  <div id="app">
    <div id="nav">
      <TopHeader/>
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link>
    </div>
    <router-view/>
    <Footer/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import TopHeader from "@/components/TopHeader.vue";
import Footer from "@/components/Footer.vue";
@Component({
    components: {
        TopHeader,
        Footer
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
</style>
