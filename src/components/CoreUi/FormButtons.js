import React from 'react'
import PropTypes from 'prop-types'

export const FormButtons = ({firstBtnTitle, secondBtnFunction}) => {
    return (
        <div>
          <button type="submit" className="mt-2 btn btn-primary">{firstBtnTitle}</button>
          <button className="mt-2 ml-2 btn btn-primary" onClick={secondBtnFunction}>Cancel/ Return</button>
        </div>
    )
}

FormButtons.propTypes = {
    firstBtnTitle: PropTypes.string.isRequired,
    secondBtnFunction: PropTypes.func.isRequired
}
