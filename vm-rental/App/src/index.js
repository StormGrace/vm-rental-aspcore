import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import Counter from './components/counter';

import { hot } from 'react-hot-loader';

const rootElement = document.getElementById('root');

const HotApp = hot(module)(App);

ReactDOM.render(<Counter />, rootElement);
