
const poolColors = function (a) {
    let pool = [];
    for (i = 0; i < a; i++) {
        pool.push(dynamicColors());
    }
    return pool;
}

const dynamicColors = function () {
    let r = Math.floor(Math.random() * 255);
    let g = Math.floor(Math.random() * 255);
    let b = Math.floor(Math.random() * 255);
    return "rgb(" + r + "," + g + "," + b + ")";
    
}

function colors() {
    return [
        '#F44336',
        '#E91E63',
        '#9C27B0',
        '#673AB7',
        '#3F51B5',
        '#2196F3',
        '#03A9F4',
        '#00BCD4',
        '#009688',
        '#4CAF50',
        '#8BC34A',
        '#CDDC39',
        '#F9A825',
        '#FFC107',
        '#FF9800',
        '#FF5722',
        '#795548',
        '#607D8B',
        '#9E9E9E',
        '#212121',
        '#01579B',
        '#1DE9B6',
        '#00C853',
        '#AEEA00'
    ]
}

function createChart(el, type, data, labels, title, dsTitle, random) {
    const colorsArray = random ? colors() : "#d40416";
    const chart = new Chart(el, {
        type: type,
        data: {
            labels,
            datasets: [
                {
                    label: dsTitle,
                    backgroundColor: colorsArray,
                    borderColor: colorsArray,
                    data
                }
            ]
        },
        //onResize: function (myChart, size) {
        //    var showTicks = (size.height < 170) ? false : true;
        //    myChart.options = {
        //        scales: {
        //            xAxes: [{
        //                ticks: {
        //                    display: showTicks
        //                }
        //            }]
        //        }
        //    };
        //},
        //options: options
        options: {
            responsive: true,
            maintainAspectRatio: true,
            aspectRatio: 0.8,
            title: {
                display: true,
                text: title
            },
            //scales: {
            //    yAxes: [
            //        {
            //            scaleLabel: {
            //                display: true,
            //                labelString: title
            //            },
            //            ticks: {
            //                display: true,
            //                beginAtZero: true
            //            }
            //        }
            //    ]
            //},
            //tooltips: {
            //    mode: 'label',
            //    callbacks: {
            //        labelColor: function (tooltipItem, chart) {
            //            return {
            //                borderColor: 'rgb(255, 0, 0)',
            //                backgroundColor: 'rgb(255, 0, 0)'
            //            };
            //        },
            //        label: function (t, d) {
            //            let label;
            //            switch (chartUnitType) {
            //                case 'pounds':
            //                    label = ` ${t.value} lbs`;
            //                    break;
            //                case 'currency':
            //                    label = `$ ${t.value}`
            //                    break;
            //                default:
            //                    label = ` ${t.value}`;
            //                    break;
            //            }

            //            return label;
            //        }
            //    }
            //},
            plugins: {
                labels: {
                    render: function (args) {
                        if (!args.value)
                            return '0%';
                        let max = 0; //This is the default 100% that will be used if no Max value is found
                        try {
                            //Try to get the actual 100% and overwrite the old max value
                            Object.values(data).map((num) => {
                                return max = max + num; //Convert num to integer
                            });
                        } catch (e) { }
                        return `${round((args.value * 100 / max), 2)}%`; //Calculate percent
                    },
                    textMargin: 4,
                    fontSize: 9,
                    fontColor: "#EEEEEE"
                }
            }
        }
    });
}

function round(num, decimales) {
    var signo = (num >= 0 ? 1 : -1);
    num = num * signo;
    if (decimales === 0) //con 0 decimales
        return signo * Math.round(num);
    // round(x * 10 ^ decimales)
    num = num.toString().split('e');
    num = Math.round(+(num[0] + 'e' + (num[1] ? (+num[1] + decimales) : decimales)));
    // x * 10 ^ (-decimales)
    num = num.toString().split('e');
    return signo * (num[0] + 'e' + (num[1] ? (+num[1] - decimales) : -decimales));
}