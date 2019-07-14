<template>
  <div id="app">
    <main>
      <section id="parameters-container">
        <div id="singular-parameters-container" class="field">
          <b-field 
            v-for="(parameter, key) in parameters"
            v-if="key != 'cashflows'"
            v-bind:label="parameter.label"
            v-bind:key="`input-${parameter.label}`"
            v-bind:message="parameter.message"
            v-bind:type="parameter.message != '' ? 'is-danger' : ''">
            <b-input type="number" v-model="parameter.value" step="0.01"></b-input>
          </b-field>
        </div>
        <div id="cashflow-inputs-container" class="field">
          <b-field
            v-bind:label="parameters.cashflows.label"
            v-bind:message="parameters.cashflows.message"
            v-bind:type="parameters.cashflows.message != '' ? 'is-danger' : ''">
          </b-field>
          <div class="wrapper">
            <div class="_list">
              <cashflow-input
                v-for="(cashflow, i ) in parameters.cashflows.value"
                v-bind:key="`cashflow-input-${i + 1}`"
                v-bind:number="i + 1"
                v-bind:value.sync="cashflow.value"
                v-on:cashflow-input-remove-btn-clicked="removeCashflowInput(i)"
              ></cashflow-input>
              <button
                id="btn-cashflow-add"
                class="button has-icons-left is-expanded"
                v-on:click="addCashflowInput"
              >
                <b-icon pack="fas" icon="plus" size="is-small"></b-icon>
                <span>Add Cashflow</span>
              </button>
            </div>
          </div>
        </div>
        <div id="parameter-actions-container">
          <b-button v-on:click="calculate">Calculate</b-button>
          <b-button v-on:click="reset">Reset</b-button>
        </div>
      </section>

      <section id="result-container">
        <table class="table is-striped">
          <thead>
            <tr>
              <th>Discount Rate</th>
              <th>NPV</th>
            </tr>
          </thead>
          <tbody v-if="output">
            <tr v-for="result in output.ResultSet">
              <td>{{ result.DiscountRate }}</td>
              <td>{{ result.NPV }}</td>
            </tr>
          </tbody>
        </table>
      </section>

      <section id="history-container">
        <strong class="_title is-size-6">History</strong>
        <div class="wrapper">
          <div class="_list">
            <div v-if="historyIsLoading">Fetching previously ran calculations...</div>
            <div v-else id="history-items-container" class="content">
              <history-item 
                v-for="historyItem in historyItems"
                v-bind="historyItem"
                v-on:click.native="viewHistoryItem(historyItem, $event)">
              </history-item>
            </div>
          </div>
        </div>
      </section>
    </main>
  </div>
</template>

<script>
import historyItem from "./components/HistoryItem";
import cashflowInput from "./components/CashflowInput";
export default {  
  name: "app",
  data: () => {
    return {
      parameters: {
        initialValue: {
          label : "Initial Value",
          value : 0,
          message : "",
          validator : function(context){
            if(this.value <= 0 || this.value > 10000000000) return { valid: false, message : "Valid values ranges from 1 to 10000000000"};

            return { valid : true };
          }
        },
        lowerBoundDiscountRate: {
          label : "Lower Bound Discount Rate",
          value : 0,
          message : "",
          validator : function(context){
            debugger;
            const parsedValue = parseFloat(this.value);

            if(parsedValue >= parseFloat(context.parameters.upperBoundDiscountRate.value)) return { valid : false, message : "Lower bound discount rate should be lower than Upper bound discount rate."};
            if(parsedValue < 1 || parsedValue > 100) return { valid: false, message : "Valid values ranges from 1 to 100"};

            return { valid : true };
          }
        },
        upperBoundDiscountRate: {
          label : "Upper Bound Discount Rate",
          value : 0,
          message : "",
          validator : function(context){
            debugger;
            const parsedValue = parseFloat(this.value);
            if(parsedValue <= parseFloat(context.parameters.lowerBoundDiscountRate.value)) return { valid : false, message : "Upper bound discount rate should be higher than Lower bound discount rate."};
            if(parsedValue < 1 || parsedValue > 100) return { valid: false, message : "Valid values ranges from 1 to 100"};

            return { valid : true };
          }
        },
        discountRateIncrement: {
          label : "Discount Rate Increment",
          value : 0,
          message : "",
          validator : function(context){
            if(this.value < 1 || this.value > 100) return { valid: false, message : "Valid values ranges from 1 to 100"};

            return { valid : true };
          }
        },
        cashflows: {
          label : "Cashflows",
          value : [{ value: 0 }],
          message : "",
          validator : function(context){
            const flag = this.value.some(x => x.value < -10000000000 || x.value > 10000000000);

            if(flag) return { valid : false, message : "Valid value ranges from -10000000000 to 10000000000"}

            return { valid : true }
          }
        }
      },
      output: null,
      historyItems: [],
      historyIsLoading: true
    };
  },
  methods: {
    addCashflowInput: function() {
      this.parameters.cashflows.value.push({ value: 0 });
    },
    calculate: function() {
      const that = this;
      const parameters = {
        InitialValue: that.parameters.initialValue.value,
        LowerBoundDiscountRate: that.parameters.lowerBoundDiscountRate.value,
        UpperBoundDiscountRate: that.parameters.upperBoundDiscountRate.value,
        DiscountRateIncrement: that.parameters.discountRateIncrement.value,
        Cashflows: that.parameters.cashflows.value.map(x => x.value)
      };
      const flag = this.validateInputs();
      if(!flag) return;

      this.$axios({
        url: "/api/calculate",
        method: "POST",
        data: parameters
      }).then(res => {
        that.output = res.data;
        that.historyItems.unshift(res.data);
      });
    },
    reset : function(){
      this.parameters.initialValue.value = 0;
      this.parameters.lowerBoundDiscountRate.value = 0;
      this.parameters.upperBoundDiscountRate.value = 0;
      this.parameters.discountRateIncrement.value = 0;
      this.parameters.cashflows.value = [{ value:0 }];
    },
    viewHistoryItem : function(historyItem, e){
      this.parameters.initialValue.value = historyItem.InitialValue;
      this.parameters.discountRateIncrement.value = historyItem.DiscountRateIncrement;
      this.parameters.lowerBoundDiscountRate.value = historyItem.LowerBoundDiscountRate;
      this.parameters.upperBoundDiscountRate.value = historyItem.UpperBoundDiscountRate;

      this.parameters.cashflows.value = historyItem.Cashflows.map(hi => {
        return {
          value : hi.Value
        }
      })
      
      this.output = historyItem;
      this.selectHistoryItem(e);
    },
    selectHistoryItem : function(e){
      let el = e.target;
      if(!el.classList.contains('history-item')){
        el = el.closest('.history-item');
      }
      const historyItems = document.querySelectorAll('.history-item');
      historyItems.forEach(hi => {
        hi.classList.remove('selected');
      });

      el.classList.add('selected');
    },
    removeCashflowInput : function(index){
      if(this.parameters.cashflows.value.length == 1) return;
      this.parameters.cashflows.value.splice(index, 1)
    },
    validateInputs : function(){
      for(let key in this.parameters){
        const result = this.parameters[key].validator(this);
        this.parameters[key].message = result.message || "";

        if(!result.valid) return false;
      }

      return true;
    }
  },
  components: {
    historyItem,
    cashflowInput
  },
  mounted: function() {
    this.$axios({
      url: "/api/history"
    }).then(res => {
      this.historyItems = res.data;
      this.historyIsLoading = false;
    });
  }
};
</script>

<style>
* {
  padding: 0;
  margin: 0;
}

html,
body,
#app {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
}

#app {
  margin: 1em;
}

main {
  display: flex;
  height: 100%;
  min-width: 800px;
}

main > * {
  padding: 0.5em;
}

#parameters-container {
  width: 250px;
  display: flex;
  flex-direction: column;
}

#result-container {
  flex: 1;
  overflow:auto;
}

#result-container table {
  width: 100%;
}

#result-container table th{
  position: sticky;
  top: -10px;
  background: white;
}

#cashflow-inputs-container {
  max-height: 100%;
  position: relative;
  flex: 1;
  display: flex;
  flex-direction: column;
}

#cashflow-inputs-container .wrapper{
  position: relative;
  height: 100%;
  overflow: auto;
}

#cashflow-inputs-container ._list {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  flex-direction: column;
  padding: 0.25em;
}

#cashflow-inputs-container ._list > *:not(:last-child) {
  margin-bottom: 0.5em;
}

#btn-cashflow-add{
  position: sticky;
  bottom: 0;
}

#history-container {
  position: relative;
  display: flex;
  flex-direction: column;
  width:300px;
}

#history-container .wrapper {
  position: relative;
  height: 100%;
}

#history-container ._list {
  padding: 0.5em;
  position: absolute;
  top: 0;
  right: 0;
  left: 0;
  bottom: 0;
  overflow: auto;
}

#history-container ._title {
  margin-bottom: 0.5em;
}

#parameter-actions-container > .button {
  width: 50%;
}
</style>
