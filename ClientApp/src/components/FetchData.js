import React, { Component } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = {
      request: {
        "investmentDate":"2016-11-14",
        "cdbRate": 103.5,
        "currentDate":"2016-12-23"
      }, result: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(result) {
    return (
      <LineChart width={600} height={300} data={result} margin={{top: 5, right: 30, left: 20, bottom: 5}}>
        <XAxis dataKey="date"/>
        <YAxis/>
        <CartesianGrid strokeDasharray="3 3"/>
        <Tooltip/>
        <Legend />
        <Line type="monotone" dataKey="unitPrice" stroke="#8884d8" activeDot={{r: 8}}/>
      </LineChart>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.request);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('home', this.state.request)
    const data = await response.json().then((x) => console.log(x));
    this.setState({ result: data, loading: false });
  }
}
