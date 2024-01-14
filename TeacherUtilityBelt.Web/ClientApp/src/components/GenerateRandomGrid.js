import React, { Component } from 'react';

export class GenerateRandomGrid extends Component {
  static displayName = GenerateRandomGrid.name;

  constructor(props) {
    super(props);
    this.state = { grid: {}, loading: true };
  }

  componentDidMount() {
    this.generateRandomGrid();
  }



  static renderGrid(randomGrid) {

    let grid = randomGrid.grid;
    let gridAnswer = randomGrid.gridAnswer;

    return (
      <div className="grid">
        <div>
        <table className="table" aria-labelledby="tableLabel">
          <tbody>
          {
            grid.map((row, i) => (
              <tr>
                { 
                  row.map((col, j) => ( <td>{grid[i][j]}</td> ) )
                }
              </tr>
            ))}
          </tbody>
        </table>
        </div>
        <div className="answers">
          {Object.keys(gridAnswer).map((key, index) => (
              <div>
                <div>Found word: {key}</div>
                {Object.keys(gridAnswer[key]).map((y, i) => (
                    <div>
                      <label>At</label> {gridAnswer[key][y].x} {gridAnswer[key][y].y}
                    </div>    
                ))}
              </div>
            ))}
        </div>
      </div>
    );
  }

  render() {
    let content = this.state.loading
    ? <p><em>Loading...</em></p>
    : GenerateRandomGrid.renderGrid(this.state.grid);

    return (
        <div>
        {content}
        </div>
    );
  }

  async generateRandomGrid() {

    const response = await fetch('FindWord/GetGridAnswerResponse');
    const data = await response.json();
    this.setState({ grid: data, loading: false });
  }
}