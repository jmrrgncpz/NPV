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
        <div id="cashflow-inputs-container" class="field">
          <p class="label is-size-6">Cashflows</p>
          <div class="wrapper">
            <div class="_list">
              <b-input
                v-for="cashflow in parameters.cashflows"
                type="number"
                placeholder="Cashflow"
                v-model="cashflow.value"
              ></b-input>
              <button
                class="button has-icons-left is-expanded btn-cashflow-add"
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

export default {
  name: "app",
  data: () => {
    return {
      parameters: {
        initialValue: 0,
        lowerBoundDiscountRate: 0,
        upperBoundDiscountRate: 0,
        discountRateIncrement: 0,
        cashflows: [{ value: 0 }]
      },
      output: null,
      historyItems: [],
      historyIsLoading: true
    };
  },
  methods: {
    addCashflowInput: function() {
      this.parameters.cashflows.push({ value: 0 });
    },
    calculate: function() {
      const that = this;
      const parameters = {
        InitialValue: that.parameters.initialValue,
        LowerBoundDiscountRate: that.parameters.lowerBoundDiscountRate,
        UpperBoundDiscountRate: that.parameters.upperBoundDiscountRate,
        DiscountRateIncrement: that.parameters.discountRateIncrement,
        Cashflows: that.parameters.cashflows.map(x => x.value)
      };
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
      this.parameters.initialValue = 0;
      this.parameters.lowerBoundDiscountRate = 0;
      this.parameters.upperBoundDiscountRate = 0;
      this.parameters.discountRateIncrement = 0;
      this.parameters.cashflows = [{ value:0 }];
    },
    viewHistoryItem : function(historyItem, e){
      this.parameters.initialValue = historyItem.InitialValue;
      this.parameters.discountRateIncrement = historyItem.DiscountRateIncrement;
      this.parameters.lowerBoundDiscountRate = historyItem.LowerBoundDiscountRate;
      this.parameters.upperBoundDiscountRate = historyItem.UpperBoundDiscountRate;

      this.parameters.cashflows = historyItem.Cashflows.map(hi => {
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
    }
  },
  components: {
    historyItem
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
}

#cashflow-inputs-container ._list > *:not(:last-child) {
  margin-bottom: 0.5em;
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
