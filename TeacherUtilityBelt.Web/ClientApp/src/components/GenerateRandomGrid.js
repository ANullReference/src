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

  static renderGrid(grid) {
    return (
      <div></div>
    );
  }

  render() {
    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : GenerateRandomGrid.renderGrid(this.state.grid);

    return (
        <div>
        {contents}
        </div>
    );
  }

  async generateRandomGrid() {
    console.log('hello');
    const response = await fetch('FindWord');
    const data = await response.json();
    this.setState({ grid: data, loading: false });
  }
}
