<template>
    <div class="root">
        <div class="values">
            <b-form-input @focus="isClicked(fromClick)" @focusout="isNotClicked(fromClick)" v-model="from" tabindex="0"></b-form-input>
            <b-form-input @focus="isClicked(toClick)" @focusout="isNotClicked(toClick)" class="to" v-model="to" tabindex="0"></b-form-input>
        </div>
        <div class="range-slider">
            <div class="range-slider-group">
                <div class="span-min" :style="'left:0;right:' + (100 - spanMin) + '%'"></div>
                <div class="span-gap" :style="'left:' + (spanMin) +'%;right:' + (100 - spanMax) + '%'"></div>
                <div class="span-max" :style="'left:'+ (spanMax) + '%;right:0;'"></div>

                <input type="range" v-model="from" @mouseup="sendRequest()" class="slider-min" :min="minValue" :max="maxValue" step="50" />
                <input type="range" v-model="to" @mouseup="sendRequest()" class="slider-max" :min="minValue" :max="maxValue" step="50" />
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import { Query } from "../types/query";

@Component
export default class RangeSlider extends Vue {
    @Prop() minValue!: number;
    @Prop() maxValue!: number;
    @Prop() fromValue!: number;
    @Prop() toValue!: number;

    queries = Array<Query>();
    to = Number(this.toValue);
    from = Number(this.fromValue);
    spanMin = 0;
    spanMax = 0;
    moving = 0;
    gap = 100;
    toClick = false;
    fromClick = false;

    @Watch("to") f() {
        this.moving = 0;

        if (this.toClick) {
            this.sendRequest();
        }
    }

    @Watch("from") g() {
        this.moving = 1;

        if (this.fromClick) {
            this.sendRequest();
        }
    }

    updated() {
        this.moveSliders();
        this.constrainMinMax();
        this.updateSpans();
    }

    mounted() {
        this.updateSpans();
    }

    updateSpans() {
        this.spanMin = (this.from - this.minValue) / (this.maxValue - this.minValue) * 100;
        this.spanMax = (this.to - this.minValue) / (this.maxValue - this.minValue) * 100;
    }

    moveSliders() {
        if (this.moving === 1 && Number(this.to) - this.gap < this.from) {
            this.to = Number(this.from) + this.gap;
        }
        if (this.moving === 0 && Number(this.from) + this.gap > this.to) {
            this.from = Number(this.to) - this.gap;
        }
    }

    constrainMinMax() {
        if (Number(this.from) < this.minValue) {
            this.from = this.minValue;
        }
        if (Number(this.to) < this.minValue) {
            this.to = this.minValue;
        }
        if (Number(this.from) > this.maxValue) {
            this.from = this.maxValue;
        }
        if (Number(this.to) > this.maxValue) {
            this.to = this.maxValue;
        }

        const startGap = Number(this.from) + this.gap;
        const endGap = Number(this.to) - this.gap;

        if (this.from > endGap) {
            this.from = endGap;
        }
        if (this.to < startGap) {
            this.to = startGap;
        }
    }

    isClicked() {
        this.toClick = true;
        this.fromClick = true;
    }

    isNotClicked() {
        this.fromClick = false;
        this.toClick = false;
    }

    sendRequest() {
        this.NewQuery("minPrice", this.from.toString());
        this.NewQuery("maxPrice", this.to.toString());
        this.ReplaceRoute(this.queries);
    }

    NewQuery(name: string, value: string) {
        const newQuery = {} as Query;
        newQuery.name = name;
        newQuery.value = value;
        this.queries = this.$store.state.queries;
        this.queries = this.DeleteFromQueryList(this.queries, name);
        this.queries.push(newQuery);
        this.$store.commit("queriesState", this.queries);
    }

    DeleteFromQueryList(array: Array<Query>, name: string) {
        for (let i = array.length - 1; i >= 0; i--) {
            if (array[i].name === name) {
                array.splice(i, 1);
            }
        }
        return array;
    }

    ReplaceRoute(parameters: Array<Query>) {
        if (this.$route.name === "Overview") {
            this.$router.replace({ query: { payload: JSON.stringify(parameters) } });
        } else {
            this.$router.replace({ name: "Overview", query: { payload: JSON.stringify(parameters) } });
        }
    }
}
</script>

<style lang="scss" scoped>
@import "../assets/main.scss";

@mixin slider-track {
    pointer-events: none;
    position: absolute;
    padding: 0;
    margin: 0;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    height: 100%;
    width: 100%;
    border-radius: 1em;
    background: none;
    border: 0;
}

@mixin slider-thumb {
    pointer-events: all;
    position: relative;
    z-index: 9;
    outline: 0;
    width: 1em;
    height: 1.5em;
    z-index: 10;
    background: #007bff;
    box-shadow: 0 .15em .15em $light;
    border-radius: 0.3em;
    cursor: grab;
    border: 0;

    &:active {
        cursor: grabbing;
    }
}

.range-slider {
    position: relative;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: center;
    padding: 0 10px 1.5em 10px;

    input {
        -webkit-appearance: none;
        -moz-appearance: none;
        z-index: 1;
        @include slider-track();

        &:active,
        &:focus {
            outline: 0;
            z-index: 2;
        }
        &::-moz-focus-outer {
            border: 0;
        }
        &::-webkit-slider-runnable-track {
            margin: 0 -.5em;
        }
        &::-moz-range-track {
            @include slider-track();
        }
        &::-webkit-slider-thumb {
            -webkit-appearance: none;
            @include slider-thumb();
        }
        &::-moz-range-thumb {
            -moz-appearance: none;
            @include slider-thumb();
        }
    }
}
.range-slider-group {
   position: relative;
   flex: 1;
   height: 1em;
   border-radius: 1em;
   pointer-events: none;

   [class*='span-'] {
        pointer-events: none;
        position: absolute;
        top: 0;
        bottom: 0;
        &.span-min {
            border-radius: .2em 0 0 .2em;
        }
        &.span-gap[style*='left:0%'] {
            border-top-left-radius: 1em;
            border-bottom-left-radius: 1em;
        }
        &.span-gap[style*='right:0%'] {
            border-top-right-radius: 1em;
            border-bottom-right-radius: 1em;
        }
        &.span-max {
            border-radius: 0 .2em .2em 0;
        }
    }
    .span-min {
        background: $light;
    }
    .span-gap {
        background: $primary;
    }
    .span-max {
        background: $light;
    }
}
.values {
    margin: 4% 4%;
    display: flex;
    flex-wrap: wrap;
    input {
        width: 40%;
    }
    .to {
        margin: 0 0 0 20%;
    }
}
</style>
