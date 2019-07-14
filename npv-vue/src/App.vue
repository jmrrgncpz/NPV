<template>
  <div id="app">
    <main>
      <section id="parameters-container">
        <div id="singular-parameters-container">
          <b-field label="Initial Value">
            <b-input v-model="parameters.initialValue" type="number"></b-input>
          </b-field>
          <div class="discount-rate-range">
            <b-field label="Lower Bound Discount Rate">
              <b-input v-model="parameters.lowerBoundDiscountRate" type="number"></b-input>
            </b-field>
            <b-field label="Upper Bound Discount Rate">
              <b-input v-model="parameters.upperBoundDiscountRate" type="number"></b-input>
            </b-field>
          </div>
          <b-field label="Discount Rate Increment">
            <b-input v-model="parameters.discountRateIncrement" type="number" step="0.01"></b-input>
          </b-field>
        </div>
        <div id="cashflow-inputs-container">
          <div class="wrapper">
            <span class="title is-size-6">Cashflows</span>
            <b-input v-for="cashflow in parameters.cashflows" type="number"  placeholder="Cashflow" v-model="cashflow.value"></b-input>
            <button class="button has-icons-left is-expanded btn-cashflow-add" v-on:click="addCashflowInput">
              <b-icon pack="fas" icon="plus" size="is-small"></b-icon>
              <span>Add Cashflow</span>
            </button>
          </div>
        </div>
        <div id="parameter-actions-container">
          <b-button v-on:click="calculate">Calculate</b-button>
          <b-button>Reset</b-button>
        </div>
      </section>

      <section id="result-container">
        <table>
          <thead>
            <tr>
              <th>Discount Rate</th>
              <th>NPV</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="result in output">
              <th>{{ result.DiscountRate }}</th>
              <th>{{ result.NPV }}</th>
            </tr>
          </tbody>
        </table>
      </section>
      
      <section id="history-container">
        <span class="title is-size-6">History</span>
        <div v-if="historyIsLoading">Fetching previously ran calculations...</div>
        <div v-else id="history-items-container" class="content">
          <history-item v-for="historyItem in historyItems" v-bind="historyItem">
          </history-item>
        </div>
      </section>
    </main>
  </div>
</template>

<script>
import historyItem from "./components/HistoryItem";

export default {
  name: 'app',
  data : () => {
    return {
      parameters : {
        initialValue : 0,
        lowerBoundDiscountRate : 0,
        upperBoundDiscountRate : 0,
        discountRateIncrement : 0,
        cashflows : [{value : 0}]
      },
      output : [],
      historyItems : [],
      historyIsLoading : true
    }
  },
  methods : {
    addCashflowInput: function(){
      this.parameters.cashflows.push({value: 0});
    },
    calculate : function(){
      const that = this;
      const parameters = {
          InitialValue : that.parameters.initialValue,
          LowerBoundDiscountRate : that.parameters.lowerBoundDiscountRate,
          UpperBoundDiscountRate : that.parameters.upperBoundDiscountRate,
          DiscountRateIncrement : that.parameters.discountRateIncrement,
          Cashflows : that.parameters.cashflows.map(x => x.value)
        };
      this.$axios({
        url : "/api/calculate",
        method : "POST",
        data : parameters,
      }).then(res => {
        that.output = res.data;
      });
    }
  },
  components : {
    historyItem
  },
  mounted : function(){
    this.$axios({
      url : "/api/history"
    }).then(res => {
       this.historyItems = res.data;
       this.historyIsLoading = false;
    });
  }
}
</script>

<style>
*{
  padding: 0;
  margin : 0;
}

html,
body,
#app{
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
}

#app{
  padding: 1em;
}

#parameters-container{
  display: flex;
  position: relative;
}

#parameters-container > *:not(:last-child){
  margin-right: 1em;
}

#singular-parameters-container{
  width: 300px;
}

#parameter-actions-container{
  margin-left: auto;
}

#cashflow-inputs-container{
  position: relative;
  width: 200px;
}

#cashflow-inputs-container .wrapper{
  display: flex;
  flex-direction: column;
  position: absolute;
  top: 0;
  right: 0;
  bottom:0;
  left: 0;
  overflow: auto;
}

#cashflow-inputs-container .wrapper > *:not(:last-child){
  margin-bottom: .5em;
}

#history-items-container{
  display: flex;
}

#history-items-container > *:not(:last-child){
  margin-right: 1em;
  margin-bottom: 0;
}
</style>
