import React, { useState } from 'react'

const InputForm = (props) => {
    const [investmentDate, setInvestmentDate] = useState('2016-11-14')
    const [cdbRate, setCdbRate] = useState(103.5)
    const [currentDate, setCurrentDate] = useState('2016-12-23')
    const data = []

    const sendData = (investmentDate, cdbRate, currentDate) => {
        let request = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                investmentDate: investmentDate,
                cdbRate: cdbRate,
                currentDate: currentDate
            })
        }

        fetch('/api/home', request)
        .then(response => response.json())
        .then((event) => {
            props.parentCallback(event)
        });
    }

    return (
        <form
            onSubmit={event => {
                event.preventDefault()
                if (!investmentDate || !cdbRate || !currentDate) return
                sendData(investmentDate, cdbRate, currentDate)
            }}
            >
        <label>Data de investimento</label>
        <input type="text" name="investmentDate" value={investmentDate} onChange={e => setInvestmentDate(e.target.value)} />
        <label>Taxa do Cdb</label>
        <input type="text" name="cdbRate" value={cdbRate} onChange={e => setCdbRate(e.target.value)} />
        <label>Data de resgate</label>
        <input type="text" name="currentDate" value={currentDate} onChange={e => setCurrentDate(e.target.value)} />
        <button>Enviar</button>
        </form>
    )
}

export default InputForm