<template>
    <button>
        <div class="tile">
            <div class="tag">
                <p>{{ formattedPrice }} kr</p>
            </div>
            <svg viewBox="0 0 14 16">
                <path fill="currentColor" v-bind:d="path"/>
            </svg>
            <h4>{{ title }}</h4>
            <p class="description">{{ snippet }}</p>
            <div class="bottom">
                <span class="category">{{ categoryName }}</span>
                <span class="region">{{ region }}</span>
            </div>
        </div>
    </button>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { GetCategory } from "../api/category";

@Component
export default class Tile extends Vue {
    @Prop() private title!: string;
    @Prop() private price!: number;
    @Prop() private description!: string;
    @Prop() private categoryId!: number;
    @Prop() private region!: string;
    @Prop() private path!: string;

    categoryName = "";
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

    mounted() {
        this.setCategoryName(this.categoryId);
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

.tile {
    color: white;
}
.tag p {
    background: white;
    font-weight: 700;
    color: $primary;
    border-radius: 0 5px 0 5px;
    width: 100px;
    margin-left: auto;
    margin-right: 0;
}
.category {
    margin: 5px 10px;
    color: white;
    float: left;
}
.region {
    margin: 5px 10px;
    color: white;
    float: right;
}

svg {
    width: 70px;
    height: 70px;
}
button {
    background: $primary;
    width: 100%;
    border: 0;
    padding: 0;
    border-radius: 5px;
    box-shadow: 5px 0px 5px whitesmoke;
    transition: .15s;
}
button:hover {
    box-shadow: 5px 5px 10px $gray;
}
h4 {
    color: white;
    font-weight: 600;
    padding: 5px;
    margin: 0;
}

.description {
    color: white;
    padding: 0px 20px;
    margin: 0 0 30px 0;
    height: 3em;
}
</style>
