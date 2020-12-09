<template>
    <button :class="{ promo: promoted }">
        <div class="tile" :class="{ promotext: promoted }">
            <div class="tag" :class="{ promotag: promoted }">
                <p >{{ formattedPrice }} kr</p>
            </div>
            <svg v-if="path != 'error'" viewBox="0 0 16 16">
                <path fill-rule="evenodd" fill="currentColor" v-bind:d="path"/>
            </svg>
            <h4 :class="{ promotext: promoted }">{{ title }}</h4>
            <p class="description" :class="{ promotext: promoted }">{{ snippet }}</p>
            <div class="bottom">
                <span class="category" :class="{ promotext: promoted }">{{ categoryName }}</span>
                <span class="region" :class="{ promotext: promoted }">{{ region }}</span>
            </div>
        </div>
    </button>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { GetCategory, GetCategorySVG } from "../api/category";

@Component
export default class Tile extends Vue {
    @Prop() private title!: string;
    @Prop() private price!: number;
    @Prop() private description!: string;
    @Prop() private categoryId!: number;
    @Prop() private region!: string;
    @Prop() private promoted!: boolean;

    categoryName = "";
    path = "";
    snippet = this.createSnippet(this.description);
    formattedPrice = this.numberWithCommas(this.price);

    numberWithCommas(x: number) {
        return x.toString().replace(".", ",").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }

    createSnippet(text: string) {
        if (text.length > 80) {
            return text.slice(0, 80) + "...";
        }
        return text;
    }

    setCategoryName(id: number) {
        GetCategory(id).then(response => {
            this.categoryName = response.data.name;
        });
    }

    setSVGPath(name: string) {
        this.path = GetCategorySVG(name);
    }

    mounted() {
        this.setCategoryName(this.categoryId);
    }

    updated() {
        this.setSVGPath(this.categoryName);
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

.tile {
    color: $gray;
}
.tag p {
    background: $primary;
    font-weight: 700;
    color: white;
    border-radius: 0 5px 0 5px;
    width: 100px;
    margin-left: auto;
    margin-right: 0;
}
.region {
    margin: 5px 10px;
    float: right;
}
.category {
    margin: 5px 10px;
    float: left;
}
svg {
    width: 70px;
    height: 70px;
}
button {
    background: $light;
    width: 100%;
    border: 0;
    padding: 0;
    border-radius: 5px;
    box-shadow: 5px 0px 5px whitesmoke;
    transition: .1s;
}
button:hover {
    box-shadow: 5px 5px 10px rgba(0,0,0,.15);
    transform: scale(1.05);
}
h4 {
    font-weight: 600;
    padding: 5px;
    margin: 0;
}
.region {
    float: right;
}
.description {
    padding: 0px 20px;
    margin: 0 0 30px 0;
    height: 3em;
}
.promo {
    background: $primary;
}
.promotext {
    color: white;
}
.promotag p {
    color: $primary;
    background: white;
}
</style>
