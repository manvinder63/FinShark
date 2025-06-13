import { SyntheticEvent } from 'react'

interface Props {
    portfolioValue: string
    onDeleteClick : (e: SyntheticEvent) => void
}

function DeletePortfolio({ portfolioValue, onDeleteClick}: Props) {
  return (
    <form onSubmit={onDeleteClick}>
        <input type="hidden" value={portfolioValue} />
        <button>X</button>
    </form>
  )
}

export default DeletePortfolio