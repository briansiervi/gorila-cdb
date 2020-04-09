import React, { useState } from 'react';
import InputForm from './forms/InputForm';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';

const App = () => {
  const [responseData, setResponseData] = useState([
    {
      "date": "2016-11-14",
      "unitPrice": 1.0111489786000000
    },
    {
      "date": "2016-11-16",
      "unitPrice": 1.0224222569238200
    },
    {
      "date": "2016-11-17",
      "unitPrice": 1.0338212207864300
    },
    {
      "date": "2016-11-18",
      "unitPrice": 1.0453472714532000
    },
    {
      "date": "2016-11-21",
      "unitPrice": 1.0570018258122000
    },
    {
      "date": "2016-11-22",
      "unitPrice": 1.0687863165483400
    },
    {
      "date": "2016-11-23",
      "unitPrice": 1.0807021923195100
    },
    {
      "date": "2016-11-24",
      "unitPrice": 1.0927509179346500
    },
    {
      "date": "2016-11-25",
      "unitPrice": 1.1049339745338300
    },
    {
      "date": "2016-11-28",
      "unitPrice": 1.1172528597703200
    },
    {
      "date": "2016-11-29",
      "unitPrice": 1.1297090879946900
    },
    {
      "date": "2016-11-30",
      "unitPrice": 1.1423041904409700
    },
    {
      "date": "2016-12-01",
      "unitPrice": 1.1549593673373600
    },
    {
      "date": "2016-12-02",
      "unitPrice": 1.1677547463827200
    },
    {
      "date": "2016-12-05",
      "unitPrice": 1.1806918808261900
    },
    {
      "date": "2016-12-06",
      "unitPrice": 1.1937723411247900
    },
    {
      "date": "2016-12-07",
      "unitPrice": 1.2069977151340700
    },
    {
      "date": "2016-12-08",
      "unitPrice": 1.2203696083008600
    },
    {
      "date": "2016-12-09",
      "unitPrice": 1.2338896438581700
    },
    {
      "date": "2016-12-12",
      "unitPrice": 1.2475594630222000
    },
    {
      "date": "2016-12-13",
      "unitPrice": 1.2613807251916200
    },
    {
      "date": "2016-12-14",
      "unitPrice": 1.2753551081489600
    },
    {
      "date": "2016-12-15",
      "unitPrice": 1.2894843082643000
    },
    {
      "date": "2016-12-16",
      "unitPrice": 1.3037700407012100
    },
    {
      "date": "2016-12-19",
      "unitPrice": 1.3182140396249200
    },
    {
      "date": "2016-12-20",
      "unitPrice": 1.3328180584128700
    },
    {
      "date": "2016-12-21",
      "unitPrice": 1.3475838698675300
    },
    {
      "date": "2016-12-22",
      "unitPrice": 1.3625132664316000
    },
    {
      "date": "2016-12-23",
      "unitPrice": 1.3776080604056200
    }
  ]);

  const onChange = (data) => {
    setResponseData(data)
  }

  return (
    <div className="container">
      <h1>Taxa CDI Acumulada</h1>
      <div className="flex-row">
        <div className="flex-large">
            <LineChart width={1000} height={200} data={responseData} margin={{top: 5, right: 30, left: 20, bottom: 5}}>
              <XAxis dataKey="date"/>
              <YAxis/>
              <CartesianGrid strokeDasharray="3 3"/>
              <Tooltip/>
              <Legend />
              <Line type="monotone" dataKey="unitPrice" stroke="#8884d8" activeDot={{r: 8}}/>
            </LineChart>
        </div>
      </div>
      <div className="flex-row">
        <div className="flex-large">
          <InputForm parentCallback = {onChange}/>
        </div>
      </div>
    </div>
  )
}

export default App