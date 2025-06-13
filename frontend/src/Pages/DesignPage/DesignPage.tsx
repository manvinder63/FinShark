import React from 'react'
import Table from '../../Components/Table/Table'
import RatioList from '../../Components/RatioList/RatioList';

type Props = {};

function DesignPage(props: Props) {
  return (
    <>
        <h1>FinShark Design Page</h1>
        <h2>
            This is FinShark's design page. This is where we will house various design aspects of the app.
        </h2>
        <RatioList />
        <Table />
    </>
  )
}

export default DesignPage