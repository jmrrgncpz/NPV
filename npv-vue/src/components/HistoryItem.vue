<template>
    <div class="history-item box">
        <div class="initial-value">
            <b-taglist attached>
                <b-tag type="is-dark">Initial Value</b-tag>
                <b-tag type="is-info"> {{ InitialValue }} </b-tag>
            </b-taglist>
        </div>
        <div class="discount-rate-range">
            <b-taglist attached>
                <b-tag type="is-dark">Discount Rate from</b-tag>
                <b-tag type="is-info"> {{ LowerBoundDiscountRate }} </b-tag>
                <b-tag type="is-dark">to</b-tag>
                <b-tag type="is-danger"> {{ UpperBoundDiscountRate }} </b-tag>
            </b-taglist>
        </div>
        <div class="discount-rate-increment">
            <b-taglist attached>
                <b-tag type="is-dark">Discount Rate Increment</b-tag>
                <b-tag type="is-info"> {{ DiscountRateIncrement }} </b-tag>
            </b-taglist>
        </div>
        <div class="cashflows">
            <b-taglist v-for="(cashflow, i) in Cashflows" attached>
                <b-tag type="is-dark">Cashflow {{ i + 1}} </b-tag>
                <b-tag type="is-info"> {{ cashflow.Value }} </b-tag>
            </b-taglist>
        </div>
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
    padding: 0.5em;
}

.history-item > *:not(:last-child){
    margin-bottom: .25em;
}

.history-item.selected{
    background: lightgray;
}

.history-item:hover{
    background:whitesmoke;
}

.cashflows .tags{
    margin-bottom: 0;
}

.cashflows .tags .tag{
    margin-bottom: 0.25em;
}
</style>


