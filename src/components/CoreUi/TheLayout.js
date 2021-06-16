import React from 'react';
import TheSidebar from "./TheSidebar";
import TheHeader from "./TheHeader";
import TheContent from "./TheContent";
import TheFooter from "./TheFooter";

export const TheLayout = () => {
  return (
    <>
      <div className="c-app c-default-layout">
        {<TheSidebar />}
        <div className="c-wrapper">
          {<TheHeader />}
          <div className="c-body">
            <TheContent />
          </div>
          {<TheFooter />}
        </div>
      </div>
    </>)
}

