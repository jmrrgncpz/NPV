<template>
    <div class="history-item box">
        <div class="initial-value">{{ InitialValue }}</div>
        <div class="discount-rate-range">
            <span> {{ LowerBoundDiscountRate }} </span>
            <span> - </span>
            <span> {{ UpperBoundDiscountRate }} </span>
        </div>
        <div class="discount-rate-increment"> {{ DiscountRateIncrement }} </div>
    </div>
</template>
<script>
const props = {
    ID : {
        type : Number,
        required : true
    },
    CalculationDate : {
        type : String,
        required : true
    },
    Cashflows : {
        type : Array,
        required : true
    },
    DiscountRateIncrement : {
        type : Number,
        required: true
    },
    InitialValue : {
        type: Number,
        required : true
    },
    LowerBoundDiscountRate : {
        type: Number,
        required : true,
    },
    UpperBoundDiscountRate : {
        type : Number,
        required : true
    },
    ResultSet : {
        type: Array,
        required : true
    }
}

export default {
    props
}
</script>
<style scoped>
.history-item{
    cursor: pointer;
    transition: background .2s ease;
}

.history-item.selected{
    background: lightgray;
}

.history-item:hover{
    background:whitesmoke;
}
</style>


