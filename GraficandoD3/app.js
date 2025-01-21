document.getElementById('update-data').addEventListener('click', function() {
    const inputData = document.getElementById('data-input').value;
  
    const validFormat = /^(\d+)(,\d+)*$/;
    if (!validFormat.test(inputData.trim())) {
        alert("Por favor, ingresa solo números enteros separados por coma.");
      return;
    }

    const data = inputData.split(',').map(item => parseInt(item.trim()));
  
    d3.select('#chart').selectAll('*').remove();
  
    drawHorizontalBarChart(data);
  });
  
  function drawHorizontalBarChart(data) {
    const width = 500, height = 300, margin = { top: 20, right: 30, bottom: 40, left: 50 };
  
    const svg = d3.select("#chart")
                  .append("svg")
                  .attr("width", width)
                  .attr("height", height)
                  .append("g")
                  .attr("transform", `translate(${margin.left},${margin.top})`);
  
    const x = d3.scaleLinear()
                .domain([0, d3.max(data)])
                .range([0, width - margin.left - margin.right])
                .nice();
  
    const y = d3.scaleBand()
                .domain(data.map((_, i) => i)) 
                .range([0, height - margin.top - margin.bottom])
                .padding(0.1);
  
    const colors = ["#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd"];
    svg.selectAll(".bar")
       .data(data)
       .enter()
       .append("rect")
       .attr("y", (_, i) => y(i))
       .attr("x", 0)
       .attr("height", y.bandwidth())
       .attr("width", d => x(d))
       .attr("fill", (_, i) => colors[i % colors.length]);
  
    svg.selectAll(".text")
       .data(data)
       .enter()
       .append("text")
       .attr("x", d => x(d) -30) 
       .attr("y", (_, i) => y(i) + y.bandwidth() / 2)
       .attr("dy", "0.35em") 
       .text(d => d)
       .attr("fill", "black");
  }
  