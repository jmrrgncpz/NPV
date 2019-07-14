<template>
    <div class="cashflow-input box">
        <span class="cashflow-number">
            {{ number }}
        </span>
        <input class="input" type="number" v-bind:value="value" v-on:input="$emit('update:value', $event.target.value)">
        <button class="button" v-on:click="$emit('cashflow-input-remove-btn-clicked')">
            <b-icon pack="fas" icon="times" size="is-small"></b-icon>
        </button>
    </div>
</template>

<script>
const props = {
    number : {
        type : Number,
        required : true
    },
    value : {
        type : [Number, String],
        required : true
    }
}

export default {
    props,
}
</script>

<style scoped>
    .cashflow-input{
        padding: 0.25em;
        display: flex;
        align-items: center;
    }

    .cashflow-input .input{
        flex: 1;
    }

    .cashflow-number{
        padding: 0.5em;
    }
</style>
